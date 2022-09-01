using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.CScripts;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CardsMain : MonoBehaviour
{
    public SpriteRenderer bg;
    public SpriteRenderer topWater;
    public SpriteRenderer bottomWater;
    public int LevelNo=-1;
    
    private System.Random _random;
    private CardLayout _mainCamera;
    private int _levelNo;
    private CardHud _cardHud;
    private int _columns;
    private int _rows;
    private UIDocument _uiDocument;
    private UnityEngine.Object _sockCardPrefab;
    private List<SockCardPrefabScript> _sockCardPrefabs = new List<SockCardPrefabScript>();
    private int[] _selection;

    private Timer _timer;
    private bool _touchActive = true;
    //private bool _gameDone = false;
    private int moves = 10;
    private int gameState = 0; //0 runs, 1 pause, 2 lost, 3 won
    
    private BetweenLevels _betweenLevels;
    private QuickSettings _quickSettings;
    
    
    public int MoveNo
    {
        get
        {
            return moves;
        }
        set
        {
            try
            {
                

                var i = value >= 0 ? value : 0;
                var c = _sockCardPrefabs.Count / 2;
                MonsterMood mm;
                if (c>i)
                {
                    mm = MonsterMood.Sad;
                }else if (c * 1.5f > i)
                {
                    mm = MonsterMood.Excited;
                }
                else
                {
                    mm = MonsterMood.Happy;
                }
                //var m = _wmScoreboard.GetWashingMachineMood(value);
                _cardHud.updateInfo($"{i}",mm);
            }
            catch
            {
                
            }

            moves = value;
        }
    }
    
    
    void Awake()
    {
        var sgd = SerialGameData.LoadOrGenerate();
        if (LevelNo > 0)
        {
            _levelNo = LevelNo - 1;
        }
        else
        {
            

            var levelInfo = Constants.GetNextLevel(sgd.nextLevel);

            if (levelInfo.SceneName != "Cards")
            {
                throw new Exception("there is a problem");
            }
        
            _levelNo = levelInfo.LevelNo;
        }

        
        _random = new System.Random();
        var thisLevel = CardLevels.CardLevelInfos[_levelNo];
            
        _rows = thisLevel.Row;
        _columns = thisLevel.Column;

        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        
        
        _mainCamera = new CardLayout(Camera.main, _rows, _columns);
        _selection = new int[] {-1,-1};
        _timer = new Timer();

        var littleS = _mainCamera.playfieldRect().width / bg.sprite.rect.width*100f;
        bg.gameObject.transform.localScale = new Vector3(littleS, littleS, 1f);
        
        _sockCardPrefab = Resources.Load("prefabs/SockCardPrefab");
        var ssp1 = _sockCardPrefab.GetComponent<SockCardPrefabScript>();

        List<int> cardTypeList;
        if (thisLevel.CardTypes > 0)
        {
            var ctl = generateCardList(thisLevel.CardTypes,thisLevel.CardTypes);
            var r = new List<int>();
            for (int j = 0; j <ssp1.socks.Count; j++)
            {
                r.Add(j);
            }
            var shuffledSocks = r.OrderBy(_ => _random.Next()).ToList();

            cardTypeList = new List<int>();
            foreach (var i in ctl)
            {
                
                cardTypeList.Add(shuffledSocks[i]);
                
            }


        }
        else
        {
            cardTypeList = generateCardList(ssp1.socks.Count,thisLevel.CardTypes);
        }
        
        
        

        var v = (float)_random.NextDouble();
        var v2 = (float)_random.NextDouble();
        
        for (var i = 0; i < cardTypeList.Count; i++)
        {
            
            var c = i % _columns;
            var r = i / _columns;
            var s = (GameObject)Instantiate(_sockCardPrefab);
            var sc = s.GetComponent<SockCardPrefabScript>();
            sc.SelectedSockCard = cardTypeList[i];
            sc.ChangeCardBackSprite(v, v2);
            sc.Resize(_mainCamera.Centres[r,c], _mainCamera.SingleScale);
            //s.transform.position = _mainCamera.Centres[r,c];
            //s.transform.localScale = _mainCamera.SingleScale;
            _sockCardPrefabs.Add(sc);
        }
        
        
        var pfr = _mainCamera.playfieldRect();
        var left = pfr.xMin;
        var bottom = pfr.yMax;
        var tw = topWater.size;
        var bw = bottomWater.size;

        topWater.gameObject.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, 0f);
        bottomWater.gameObject.transform.position = new Vector3(left + bw.x / 2f, pfr.yMin - bw.y / 2f, 0f);

        
        _cardHud = new CardHud(_mainCamera);
        _cardHud.AddToVisualElement(_uiDocument.rootVisualElement);
        _cardHud.SettingsButtonAction = () =>
        {
            gameState = 1;
        };

        MoveNo = thisLevel.Moves;
        
        _betweenLevels = new BetweenLevels(_mainCamera);
        _betweenLevels.AddToVisualElement(_uiDocument.rootVisualElement);
        _betweenLevels.setVisible(false);
        _betweenLevels.OnCross = () =>
        {
            ToHQ();
        };
        _betweenLevels.OnBigButton = () =>
        {
        };


        
        _quickSettings = new QuickSettings(_mainCamera, sgd.sound, sgd.music);
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

        //var r = new Range(0, ssp1.socks.Count);






        /*
        var f = (_rows * _columns) / ssp1.socks.Count/2f;
        
        var cardTypes = new List<int>();

        for (int i = 0; i < f; i++)
        {
            var r = new List<int>();
            for (int j = 0; j < ssp1.socks.Count; j++)
            {
                r.Add(j);
            }
            var m = r.OrderBy(_ => _random.Next()).ToList();
            cardTypes.AddRange(m);
            
        }
        */


        // Debug.Log($"ssp1: {cardTypes.Count*2}");

    }


    private List<int> generateCardList(int totalSocksCount, int sockNeeded)
    {
        var socksCount = sockNeeded < 0 ? totalSocksCount : sockNeeded;

        
        var totalCards =( _rows * _columns)/2;
        if ((_rows * _columns) % 2 != 0)
        {
            throw new Exception("invalid row col entry");
        }
        var fullTurns = totalCards / socksCount;
        var missing = totalCards % socksCount;
        

        
        var m = Tools.IntRange(socksCount).OrderBy(_ => _random.Next()).ToList();
        var extra = m.Take(missing);

        var r2 = new List<int>();
        for (int i = 0; i < fullTurns; i++)
        {
            for (int j = 0; j < socksCount; j++)
            {
                r2.Add(j);
            }
        }
        r2.AddRange(extra);
        var c = r2.Count;
        for (var i = 0; i < c; i++)
        {
            r2.Add(r2[i]);
        }
        var m2 = r2.OrderBy(_ => _random.Next()).ToList();
        
        if (sockNeeded < 0) return m2;

        
        var shuffledSocks = Tools.IntRange(totalSocksCount).OrderBy(_ => _random.Next()).ToList();
        var cardTypeList = new List<int>();
        foreach (var i in m2)
        {
                
            cardTypeList.Add(shuffledSocks[i]);
                
        }

        return cardTypeList;
        
        
    }

    private void HandleTouch()
    {
        var thisTurnTouches = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended).ToList();

        if (thisTurnTouches.Count <= 0) return;
        var firstTouch = thisTurnTouches[0];
        var worldPoint = _mainCamera.Camera.ScreenToWorldPoint(firstTouch.position);
        
        var counter = -1;
        var broker = false;
        foreach (var sockCardPrefabScript in _sockCardPrefabs)
        {
            counter += 1;
            if(sockCardPrefabScript.sockVisible) continue;
            

            if (sockCardPrefabScript.Collides(worldPoint))
            {
                
                broker = true;
                sockCardPrefabScript.SockVisible(true);
                
                
                
                
                for (int i = 0; i < _selection.Length; i++)
                {
                    if (_selection[i] == -1)
                    {
                        _selection[i] = counter;
                        
                        break;
                    }
                }

                
            }
            if (broker)
            {
                break;
            }
            
            
        }
        
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
    
    
    private string getBigText()
    {
        return $"Level {_levelNo+1}";
    }
    
    private string getSmallText(bool levelWon)
    {

        return levelWon ? "Yarn-tastic!" : "Level failed!";
    }

    private (int number, string text)  getLevelPoints()
    {
        return (MoveNo * 10,$"  {MoveNo * 10}");
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
        
        _timer.Update(Time.deltaTime);
        _cardHud.Update();
        _betweenLevels.Update();
        _quickSettings.Update();

        if (gameState==0)
        {
            if (_sockCardPrefabs.Count == 0)
            {
                gameState = 3;
                levelDone(true);
            }

            if (MoveNo <= 0)
            {
                gameState = 2;
                levelDone(false);
            }
            
            
            //Debug.Log($" {_selection[0]}, {_selection[1]}");
            if (_selection[_selection.Length-1] != -1)
            {
                var endLoop = false;
                for (int i = 0; i < _selection.Length; i++)
                {
                    for (int j = i+1; j < _selection.Length; j++)
                    {
                        var firstType = _sockCardPrefabs[_selection[i]];
                        var secondType = _sockCardPrefabs[_selection[j]];

                        if (firstType.SelectedSockCard == secondType.SelectedSockCard)
                        {
                            _timer.addEvent(0.4f, () =>
                            {
                                firstType.ToBeDestroyed = true;
                                secondType.ToBeDestroyed = true;
                            });
                            endLoop = true;
                            break;
                        }
                    }
                    if (endLoop)
                    {
                        break;
                    }
                }
                _timer.addEvent(0.5f, () =>
                {
                    _touchActive = true;
                    MoveNo -= 1;
                    foreach (var sockCardPrefabScript in _sockCardPrefabs)
                    {
                        
                        sockCardPrefabScript.SockVisible(false);
                    }
                });

                for (var i = 0; i < _selection.Length; i++)
                {
                    _selection[i] = -1;
                }
            }
        
        

        

            if( _touchActive)
            {
                HandleTouch();
            }
        
            foreach (var sockCardPrefabScript in _sockCardPrefabs)
            {
                if (sockCardPrefabScript.ToBeDestroyed)
                {
                    Destroy(sockCardPrefabScript.gameObject);
                }
            }
            _sockCardPrefabs.RemoveAll(x => x.ToBeDestroyed);
        }else if (gameState==1)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.setVisible(true);
            }
        }
        else
        {
            if (!_betweenLevels.Active)
            {
                _betweenLevels.setVisible(true);
            }
        }
        
        

    }
}
