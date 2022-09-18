using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using RainScripts;
using UnityEngine;

public class RainMain : GameMain
{
    //public List<Prefab> 
    
    public GameObject playfield;
    public GameObject wheel;
    public GameObject bottomWater;
    public GameObject topWater;
    public int LevelIndex = -1;
    
    private float _sockSpawnTime = 1f;
    private float _sockSpawnTimer = 0f;
    //private string _baseSockPrefabPath = "prefabs/BaseSockPrefab";
    private List<SockPrefabScript> _activeSocks = new List<SockPrefabScript>();
    private RainLayout _mainCamera;
    private RainHud RainHud => (RainHud)_gameHud;
    private float _baseWheelHeight;
    private float _wheelSpeed = 1f;
    private float _wheelStartPos;
    private int _levelIndex = 0;
    private int _maxSock= -1;
    private int _moveNo = 0;
    //private float firstStop = 1f;
    private List<GameObject> _starters = new List<GameObject>();
    
    public int MoveNo
    {
        get
        {
            return _moveNo;
        }
        set
        {
            try
            {
                var i = value >= 0 ? value : 0;
                var m = _rainScoreboard.GetWashingMachineMood(value);
                RainHud.updateInfo($"{i}",m);
            }
            catch
            {
                
            }

            _moveNo = value;
        }
    }
    
    
    
    private bool _levelEnd = false;
    private RainScoreboard _rainScoreboard;
    
    void Awake()
    {
        _gameState = GameState.Loading;
        
        if (LevelIndex > 0 && !Constants.ReleaseVersion)
        {
            _levelIndex = LevelIndex-1;
        }
        else
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var levelInfo = Constants.GetNextLevel(sgd.nextLevel);
            if (levelInfo.SceneName != "Rain")
            {
                throw new Exception("there is a problem");
            }
            _levelIndex = levelInfo.LevelNo;
        }

        _levelIndex %= RainLevels.RainLevelInfos.Length;

        var topBarHeight = 320f;
        _mainCamera = new RainLayout(Camera.main,topBarHeight);
        var r = _mainCamera.playfieldRect();
        playfield.transform.localScale = new Vector3(r.width,r.height,0f);
        playfield.transform.position = new Vector3(r.center.x,r.center.y,100f);
        
        var wsr = wheel.GetComponent<SpriteRenderer>();
        _wheelStartPos = r.center.y + wsr.sprite.vertices[0].y;
        wheel.transform.position = new Vector3(r.center.x,_wheelStartPos,90f);
        _baseWheelHeight = wsr.sprite.vertices[0].y * 2;
        wsr.size = new Vector2(r.width, _mainCamera.Camera.orthographicSize*4f);
        
        InitializeMisc();
        InitializeUi<RainHud>(topBarHeight,tutorialFrames: RainLevels.Tutorial);
            
        var left = r.xMin;
        var bottom = r.yMax;
        var tw = topWater.GetComponent<SpriteRenderer>().size;
        var bw = bottomWater.GetComponent<SpriteRenderer>().size;
        topWater.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, 0f);
        bottomWater.transform.position = new Vector3(left + bw.x / 2f, r.yMin - bw.y / 2f, 0f);
        
        var thisLevel = RainLevels.RainLevelInfos[_levelIndex];
        _wheelSpeed = thisLevel.WheelSpeed;
        _sockSpawnTime = thisLevel.SockSpawnTime;
        _maxSock = thisLevel.MaxSock;
        
        _rainScoreboard = new RainScoreboard((from t in thisLevel.RainSockInfos
            where t.LevelCollect > 0
            select t).ToList());
        MoveNo = thisLevel.MoveNo;
        
        RainHud.GenerateSocks(_rainScoreboard.GetSockInfo());
        UiStartAnimation();
    }
    private void UiStartAnimation()
    {
        RainHud.StartAnimation(0f);
        foreach (var starter in _starters)
        {   
            Destroy(starter);
        }
        _timer.addEvent(.75f, () =>
        {
            _tweenHolder.newTween(.75f, alpha =>
            {
                RainHud.StartAnimation(Math.Clamp(alpha*1.2f,0f,1f));
            }, () =>
            {
                RainHud.StartAnimation(1f);
                _gameState = GameState.Standby;
                    
                RainHud.ClearBg();

                _timer.addEvent(0.3f, () =>
                {
                    _gameState = GameState.Game;
                });
                
            });
        });
    }
    
    
    private void HandleTouch()
    {
        var thisTurnTouches1 = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended);

        var r = _mainCamera.playfieldRect(CameraTools.CoordSystem.Screen);

        var thisTurnTouches2 =
            Array.FindAll(thisTurnTouches1, x => r.Contains(new Vector2(x.position.x, x.position.y)));


        var thisTurnTouches = thisTurnTouches2.ToList();
        
        if (thisTurnTouches.Count <= 0) return; // if there is no touch to be handled, break
        foreach (var sockPrefabScript in _activeSocks) // look at each sock with each touch, remove touches and socks that collide starting from the top sock and first touch
        {
            var touched = false;
            for (var i = 0; i < thisTurnTouches.Count; i++)
            {
                var worldPoint = _mainCamera.Camera.ScreenToWorldPoint(thisTurnTouches[i].position);
                if (!sockPrefabScript.Collides(worldPoint)) continue; // if no collision continue
                touched = true;
                thisTurnTouches.RemoveAt(i);
                _rainScoreboard.IncreseSock( sockPrefabScript.style,sockPrefabScript.no );
                MoveNo -= 1;
                
                break;
            }
            if (!touched) continue;
            sockPrefabScript.Kill();
        }

        var c = _rainScoreboard.GetCollected();
        //for (var i = 0; i < _rainScoreboard.Collected.Length; i++)
        //{
        //    if (c[i] < 0)
        //    {
        //        _rainScoreboard.Collected[i] = c[i];   
        //    }
        //}
        RainHud.UpdateSocks(_rainScoreboard.GetSockInfo());
    }


    

    protected override (int number, string text)  GetLevelPoints()
    {
        return (_moveNo * 10,$"  {_moveNo * 10}");
    }

    private void FixedUpdate()
    {
        if (_gameState == GameState.Game)
        {
            Tools.TranslatePosition(wheel, y: -Time.deltaTime * _wheelSpeed);
            if (wheel.transform.position.y < _wheelStartPos - _baseWheelHeight)
            {
                Tools.MutatePosition(wheel, y: wheel.transform.position.y + _baseWheelHeight);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RainHud.Update();
        _timer.Update(Time.deltaTime);
        _tweenHolder.Update(Time.deltaTime);
        
        if (_rainScoreboard.GameWon()&& _gameState == GameState.Game)
        {
            _gameState = GameState.Won;
            LevelDone(true);
            
        }else if (MoveNo <= 0 && _gameState == GameState.Game)
        {
            _gameState = GameState.Lost;
            LevelDone(false);
        }

        if (_gameState==GameState.Won || _gameState==GameState.Lost)
        {
            foreach (var sockPrefabScript in _activeSocks.Where(sockPrefabScript => sockPrefabScript.ToBeDestroyed))
            {
                Destroy(sockPrefabScript.gameObject);
            }
            _activeSocks.RemoveAll(x => x.ToBeDestroyed);

            if (!_betweenLevels.Active)
            {
                _betweenLevels.setVisible(true);
            }
        }else if (_gameState == GameState.Settings)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.SetVisible(true);
            }
            _quickSettings.TutorialRoll(Time.deltaTime);
        }
        else if(_gameState == GameState.Game)
        {
            var i = 0;
            _sockSpawnTimer += Time.deltaTime;
            if (_sockSpawnTimer > _sockSpawnTime)
            {
                _sockSpawnTimer %= _sockSpawnTime;
                if (_maxSock <= 0 || _activeSocks.Count < _maxSock)
                {
                    i += 1;
                    var x =(float)_random.NextDouble() * 0.8f+ .1f;
                    _activeSocks.Add(generateSock(new Vector2(x: x, y: _mainCamera.playfieldTop*1.1f)));
                    ArrangeActiveSocks();
                }
            }
        
            
            foreach (var sockPrefabScript in _activeSocks)
            {
                sockPrefabScript.MoveDownTime();
                var p = _mainCamera.Camera.WorldToViewportPoint(sockPrefabScript.gameObject.transform.position);
                if (p.y <  0f)//mainCamera.playfieldBottom)
                {
                    i += 10;
                    sockPrefabScript.Kill(instantly:true);
                }
            }
        
            HandleTouch();
            foreach (var sockPrefabScript in _activeSocks.Where(sockPrefabScript => sockPrefabScript.ToBeDestroyed))
            {
                i += 100;
                Destroy(sockPrefabScript.gameObject);
            }
            
            
            _activeSocks.RemoveAll(x => x.ToBeDestroyed);
            ArrangeActiveSocks();


        }
        else if(_gameState == GameState.Loading)
        {   
        }
    }

    /** This script generates a sock
     * position is viewPort based, which is 0-1
     */
    SockPrefabScript generateSock(Vector2 viewPortPos)
    {
        var s = RainLevels.RainLevelInfos[_levelIndex].GetRandomSock(_random.NextDouble());
        var bsp = Instantiate(s.Resource());
        var sps = bsp.GetComponent <SockPrefabScript> ();
        sps.ChangeSprite(s.SockNo);
        sps.gameObject.transform.position = VectorTools.MutateVector3(_mainCamera.Camera.ViewportToWorldPoint(viewPortPos), z : 1f);
        sps.gameObject.transform.rotation = Quaternion.Euler(x: _random.Next(2)*180f, y: _random.Next(2)*180f, z: _random.Next(4) *90f);
        sps.fallSpeed = ((float) s.Speed) / 10f*_wheelSpeed;
        sps.no = s.SockNo;
        sps.style = s.SockType;
        return sps;
    }

    void ArrangeActiveSocks()
    {
        for (int i = 0; i < _activeSocks.Count; i++)
        {
            Tools.MutatePosition(_activeSocks[i].gameObject, z: (float) i+10f);
        }
    }
}
