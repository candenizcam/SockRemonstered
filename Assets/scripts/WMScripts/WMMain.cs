using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

public class WMMain : MonoBehaviour
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

    private UIDocument _uiDocument;
    private WMHud _wmHud;
    private BetweenLevels _betweenLevels;
    private QuickSettings _quickSettings;
    private float _baseWheelHeight;
    private float _wheelSpeed = 1f;
    private float _wheelStartPos;
    private System.Random _random;
    private int levelIndex = 0;
    private int _maxSock= -1;
    private int _moveNo = 0;
    private int gameState = 0; //0 runs, 1 pause, 2 lost, 3 won

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
        
        _random = new System.Random();        
        
        mainCamera = new WMLayout(Camera.main);

        var r = mainCamera.playfieldRect();
        
        playfield.transform.localScale = new Vector3(r.width,r.height,0f);

        playfield.transform.position = new Vector3(r.center.x,r.center.y,100f);
        

        var wsr = wheel.GetComponent<SpriteRenderer>();
        _wheelStartPos = r.center.y + wsr.sprite.vertices[0].y;
        wheel.transform.position = new Vector3(r.center.x,_wheelStartPos,90f);
        _baseWheelHeight = wsr.sprite.vertices[0].y * 2;
        wsr.size = new Vector2(r.width, mainCamera.Camera.orthographicSize*4f);
        
        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;

        _wmHud = new WMHud(mainCamera);
        _wmHud.AddToVisualElement(_uiDocument.rootVisualElement);
        _wmHud.SettingsButtonAction = () =>
        {
            gameState = 1;
        };

        
        
        _betweenLevels = new BetweenLevels(mainCamera);
        _betweenLevels.AddToVisualElement(_uiDocument.rootVisualElement);
        _betweenLevels.setVisible(false);
        _betweenLevels.OnCross = () =>
        {
            ToHQ();
        };
        _betweenLevels.OnBigButton = () =>
        {
        };


        
        _quickSettings = new QuickSettings(mainCamera, sgd.sound, sgd.music);
        _quickSettings.AddToVisualElement(_uiDocument.rootVisualElement);
        _quickSettings.setVisible(false);
        _quickSettings.SettingsButtonAction = () =>
        {
            _quickSettings.setVisible(false);
            gameState = 0;
        };

        _quickSettings.MusicButtonAction = (bool b) =>
        {
            Debug.Log("music cancelled");
            var sgd = SerialGameData.LoadOrGenerate();
            sgd.music = b ? 0 : 1;
            sgd.Save();
        };
        
        _quickSettings.SoundButtonAction = (bool b) =>
        {
            var sgd = SerialGameData.LoadOrGenerate();
            sgd.sound = b ? 0 : 1;
            sgd.Save();
        };

        _quickSettings.ReturnButtonAction = () =>
        {
            var sgd = SerialGameData.LoadOrGenerate();
            sgd.changeHearts(-1);
            sgd.Save();
            ToHQ();
        };
            
            
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
        

        //var a = new string[thisLevel.WmSockInfos.Length];

        _wmScoreboard = new WMScoreboard((from t in thisLevel.WmSockInfos
            where t.LevelCollect > 0
            select t).ToList());
        MoveNo = thisLevel.MoveNo;
        _wmHud.generateSocks(_wmScoreboard.ScoreAddressArray());

        _wmHud.adjustSocks(_wmScoreboard.Collected);


        //public float SockSpawnTime;
        //public float WheelSpeed;
        //public int MaxSock;
    }

    
    private void ToHQ()
    {
        SceneManager.LoadScene("HQ", LoadSceneMode.Single);
    }
    
    private void Restart()
    {
        var sgd = SerialGameData.LoadOrGenerate();
        var nl = Constants.GetNextLevel(sgd.nextLevel);
        SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
    }
    
    private void NextLevel()
    {
        var sgd = SerialGameData.LoadOrGenerate();
        
        var nl = Constants.GetNextLevel(sgd.nextLevel);
        SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
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
            //Destroy(sockPrefabScript.gameObject);
            
            
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


    private string getBigText()
    {
        return $"Level {levelIndex+1}";
    }
    
    private string getSmallText(bool levelWon)
    {

        return levelWon ? "Yarn-tastic!" : "Level failed!";
    }

    private (int number, string text)  getLevelPoints()
    {
        return (_moveNo * 10,$"  {_moveNo * 10}");
    }
    
    


    private void levelDone(bool won)
    {
        var lp = getLevelPoints();
        var buttonText = "NEXT";
        if (won)
        {
            gameState = 3;
            var sgd = SerialGameData.LoadOrGenerate();
            sgd.nextLevel += 1;
            sgd.coins += lp.number;
            sgd.Save();
            _betweenLevels.OnBigButton = () =>
            {
                NextLevel();

            };
        }
        else
        {
            gameState = 2;
            var sgd = SerialGameData.LoadOrGenerate();
            if (sgd.changeHearts(-1) > 0)
            {
                buttonText = "RETRY";
                _betweenLevels.OnBigButton = () =>
                {
                    Restart();
                };
            }
            else
            {
                buttonText = "RETURN";
                _betweenLevels.OnBigButton = () =>
                {
                    ToHQ();
                };
            }
            
            sgd.Save();
            
        }
        
        _betweenLevels.UpdateInfo(won, bigText: getBigText(), smallText: getSmallText(won), lp.text,buttonText);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        _betweenLevels.Update();
        _quickSettings.Update();
        _wmHud.Update();
        
        if (MoveNo <= 0 && gameState==0)
        {
            
            gameState = 2;
            levelDone(false);
        }

        if (_wmScoreboard.GameWon()&& gameState==0)
        {
            gameState = 3;
            levelDone(true);
            
        }

        if (gameState==2 || gameState==3)
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
        }else if (gameState == 1)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.setVisible(true);
            }
        }
        else if(gameState==0)
        {
            Tools.TranslatePosition(wheel,y: -Time.deltaTime*_wheelSpeed );
            if (wheel.transform.position.y < _wheelStartPos - _baseWheelHeight)
            {
                Tools.MutatePosition(wheel,y: wheel.transform.position.y+_baseWheelHeight);
            }

            sockSpawnTimer += Time.deltaTime;
            if (sockSpawnTimer > sockSpawnTime)
            {
                sockSpawnTimer %= sockSpawnTime;
                if (_maxSock <= 0 || _activeSocks.Count < _maxSock)
                {
                    var x =(float)_random.NextDouble() * 0.8f+ .1f;
                    _activeSocks.Add(generateSock(new Vector2(x: x, y: mainCamera.playfieldTop)));
                    ArrangeActiveSocks();
                }
            }
        
            foreach (var sockPrefabScript in _activeSocks)
            {
                sockPrefabScript.MoveDownTime();
                var p = mainCamera.Camera.WorldToViewportPoint(sockPrefabScript.gameObject.transform.position);
                if (p.y <  0f)//mainCamera.playfieldBottom)
                {
                    sockPrefabScript.Kill(instantly:true);
                }
            }
        
            HandleTouch();
            foreach (var sockPrefabScript in _activeSocks.Where(sockPrefabScript => sockPrefabScript.ToBeDestroyed))
            {
                Destroy(sockPrefabScript.gameObject);
            }
            
            
            _activeSocks.RemoveAll(x => x.ToBeDestroyed);
            ArrangeActiveSocks();
        }
        else
        {
            
        }
        
        
        
        
    }

    /** This script generates a sock
     * position is viewPort based, which is 0-1
     */
    SockPrefabScript generateSock(Vector2 viewPortPos)
    {
        
        // WmSockTypeLookup
        var s = WMLevels.WmLevelInfos[levelIndex].GetRandomSock(_random.NextDouble());
        
            
        var bsp = Instantiate(Resources.Load(WMLevels.WmSockTypeLookup[s.SockType]));
        var sps = bsp.GetComponent <SockPrefabScript> ();
        sps.ChangeSprite(s.SockNo);
        
        sps.gameObject.transform.position = Tools.MutateVector3(mainCamera.Camera.ViewportToWorldPoint(viewPortPos), z : 1f);
        
        sps.gameObject.transform.rotation = Quaternion.Euler(x: _random.Next(2)*180f, y: _random.Next(2)*180f, z: _random.Next(4) *90f);
        sps.gameObject.transform.localScale = new Vector3(Screen.width / 1284f, Screen.width / 1284f, 0f);
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