using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;
using Object = System.Object;

public class WMMain : GameMain
{
    //public List<Prefab> 
    
    public GameObject playfield;
    public GameObject wheel;
    public GameObject bottomWater;
    public GameObject topWater;
    public int LevelIndex = -1;
    
    private float sockSpawnTime = 1f;
    private float sockSpawnTimer = 0f;
    private string _baseSockPrefabPath = "prefabs/BaseSockPrefab";
    private List<SockPrefabScript> _activeSocks = new List<SockPrefabScript>();
    private WMLayout mainCamera;
    private WMHud _wmHud => (WMHud)_gameHud;
    private float _baseWheelHeight;
    private float _wheelSpeed = 1f;
    private float _wheelStartPos;
    private int levelIndex = 0;
    private int _maxSock= -1;
    private int _moveNo = 0;
    private float firstStop = 1f;
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
                var m = _wmScoreboard.GetWashingMachineMood(value);
                _wmHud.updateInfo($"{i}",m);
            }
            catch
            {
                
            }

            _moveNo = value;
        }
    }
    
    
    
    private bool _levelEnd = false;
    private WMScoreboard _wmScoreboard;
    
    void Awake()
    {
        _gameState = GameState.Loading;
        var sgd = SerialGameData.LoadOrGenerate();
        var levelInfo = Constants.GetNextLevel(sgd.nextLevel);
        if (levelInfo.SceneName != "WashingMachineScene")
        {
            throw new Exception("there is a problem");
        }

        levelIndex = levelInfo.LevelNo;
        if (LevelIndex > 0)
        {
            levelIndex = LevelIndex;
        }
        
        
        mainCamera = new WMLayout(Camera.main);
        var r = mainCamera.playfieldRect();
        playfield.transform.localScale = new Vector3(r.width,r.height,0f);
        playfield.transform.position = new Vector3(r.center.x,r.center.y,100f);
        
        var wsr = wheel.GetComponent<SpriteRenderer>();
        _wheelStartPos = r.center.y + wsr.sprite.vertices[0].y;
        wheel.transform.position = new Vector3(r.center.x,_wheelStartPos,90f);
        _baseWheelHeight = wsr.sprite.vertices[0].y * 2;
        wsr.size = new Vector2(r.width, mainCamera.Camera.orthographicSize*4f);
        
        InitializeMisc();
        InitializeUi<WMHud>(mainCamera);
            
        var left = r.xMin;
        var bottom = r.yMax;
        var tw = topWater.GetComponent<SpriteRenderer>().size;
        var bw = bottomWater.GetComponent<SpriteRenderer>().size;
        topWater.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, 0f);
        bottomWater.transform.position = new Vector3(left + bw.x / 2f, r.yMin - bw.y / 2f, 0f);
        
        var thisLevel = WMLevels.WmLevelInfos[levelIndex];
        _wheelSpeed = thisLevel.WheelSpeed;
        sockSpawnTime = thisLevel.SockSpawnTime;
        _maxSock = thisLevel.MaxSock;
        InitializeStartAnimation(thisLevel);
        _wmScoreboard = new WMScoreboard((from t in thisLevel.WmSockInfos
            where t.LevelCollect > 0
            select t).ToList());
        MoveNo = thisLevel.MoveNo;
        
        _wmHud.generateSocks(_wmScoreboard.ScoreAddressArray());
        _wmHud.adjustSocks(_wmScoreboard.Collected);
    }

    private void InitializeStartAnimation( WMLevelInfo thisLevel)
    {
        var step = 1f/thisLevel.WmSockInfos.Length;
        thisLevel.InitializeLevel(also: (x,a) =>
        {
            var b= Instantiate(a);
            b.transform.position = new Vector3(0f, 0f, x);
            b.transform.localScale = new Vector3(4f, 4f, 1f);
            b.transform.rotation = Quaternion.Euler(0f,0f, step*x*360f);
            _starters.Add(b);
        });
        _tweenHolder.newTween(firstStop, alpha =>
        {
            for (var i = 0; i < _starters.Count; i++)
            {
                var a = (6.28f*i)/_starters.Count;
                var t = _starters[i].transform;
                Tools.MutatePosition(t, alpha*25f*(float)Math.Cos(a), 25f*alpha*(float)Math.Sin(a));
                t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, t.rotation.eulerAngles.y, 360f*alpha + step*i*360f);
            }
        }, () =>
        {
            _gameState = GameState.Game;
            foreach (var starter in _starters)
            {   
                Destroy(starter);
            }
        });
    }
    
    
    private void HandleTouch()
    {
        var thisTurnTouches1 = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended);

        var r = mainCamera.playfieldRect(CameraTools.CoordSystem.Screen);

        var thisTurnTouches2 =
            Array.FindAll(thisTurnTouches1, x => r.Contains(new Vector2(x.position.x, x.position.y)));


        var thisTurnTouches = thisTurnTouches2.ToList();
        
        if (thisTurnTouches.Count <= 0) return; // if there is no touch to be handled, break
        foreach (var sockPrefabScript in _activeSocks) // look at each sock with each touch, remove touches and socks that collide starting from the top sock and first touch
        {
            var touched = false;
            for (var i = 0; i < thisTurnTouches.Count; i++)
            {
                var worldPoint = mainCamera.Camera.ScreenToWorldPoint(thisTurnTouches[i].position);
                if (!sockPrefabScript.Collides(worldPoint)) continue; // if no collision continue
                touched = true;
                thisTurnTouches.RemoveAt(i);
                _wmScoreboard.IncreseSock( sockPrefabScript.style,sockPrefabScript.no );
                MoveNo -= 1;
                
                break;
            }
            if (!touched) continue;
            sockPrefabScript.Kill();
        }

        var c = _wmScoreboard.GetCollected();
        for (var i = 0; i < _wmScoreboard.Collected.Length; i++)
        {
            if (c[i] < 0)
            {
                _wmHud.HandSock(i,c[i]);
                _wmScoreboard.Collected[i] = 0;   
            }
        }
        _wmHud.adjustSocks(_wmScoreboard.GetCollected());
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
        _betweenLevels.Update();
        _quickSettings.Update();
        _wmHud.Update();
        _timer.Update(Time.deltaTime);
        _tweenHolder.Update(Time.deltaTime);
        
        if (MoveNo <= 0 && _gameState == GameState.Game)
        {
            _gameState = GameState.Lost;
            LevelDone(false);
        }

        if (_wmScoreboard.GameWon()&& _gameState == GameState.Game)
        {
            _gameState = GameState.Won;
            LevelDone(true);
            
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
                _quickSettings.setVisible(true);
            }
        }
        else if(_gameState == GameState.Game)
        {

            
            var i = 0;
            
            sockSpawnTimer += Time.deltaTime;
            if (sockSpawnTimer > sockSpawnTime)
            {
                sockSpawnTimer %= sockSpawnTime;
                if (_maxSock <= 0 || _activeSocks.Count < _maxSock)
                {
                    i += 1;
                    var x =(float)_random.NextDouble() * 0.8f+ .1f;
                    _activeSocks.Add(generateSock(new Vector2(x: x, y: mainCamera.playfieldTop*1.1f)));
                    ArrangeActiveSocks();
                }
            }
        
            
            foreach (var sockPrefabScript in _activeSocks)
            {
                sockPrefabScript.MoveDownTime();
                var p = mainCamera.Camera.WorldToViewportPoint(sockPrefabScript.gameObject.transform.position);
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
        var s = WMLevels.WmLevelInfos[levelIndex].GetRandomSock(_random.NextDouble());
        var bsp = Instantiate(s.Resource());
        var sps = bsp.GetComponent <SockPrefabScript> ();
        sps.ChangeSprite(s.SockNo);
        sps.gameObject.transform.position = VectorTools.MutateVector3(mainCamera.Camera.ViewportToWorldPoint(viewPortPos), z : 1f);
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
