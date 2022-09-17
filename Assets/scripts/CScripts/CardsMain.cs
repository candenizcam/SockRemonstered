using System;
using System.Collections.Generic;
using System.Linq;
using Cards.CScripts;
using Classes;
using Unity.VisualScripting;
using UnityEngine;

public class CardsMain : GameMain
{
    public SpriteRenderer bg;
    public SpriteRenderer topWater;
    public SpriteRenderer bottomWater;
    public int LevelNo=-1;

    
    private CardLayout _mainCamera;
    private int _levelNo; private CardHud CardHud => (CardHud)_gameHud;
    private int _columns;
    private int _rows;
    private UnityEngine.Object _sockCardPrefab;
    private List<SockCardPrefabScript> _sockCardPrefabs = new List<SockCardPrefabScript>();
    private int[] _selection;
    private int _moves = 10;
    public int MoveNo
    {
        get
        {
            return _moves;
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
                CardHud.updateInfo($"{i}",mm);
            }
            catch
            {
                
            }
            _moves = value;
        }
    }
    
    
    void Awake()
    {
        _gameState = GameState.Game;
        var sgd = SerialGameData.LoadOrGenerate();
        if (LevelNo > 0 && !Constants.ReleaseVersion)
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
        
        _levelNo %= CardLevels.CardLevelInfos.Length;
        
        var thisLevel = CardLevels.CardLevelInfos[_levelNo];
        _rows = thisLevel.Row;
        _columns = thisLevel.Column;
        _mainCamera = new CardLayout(Camera.main, _rows, _columns);
        InitializeMisc();
        InitializeUi<CardHud>(tutorialFrames: CardLevels.Tutorial);
        _selection = new[] {-1,-1};
        var littleS = _mainCamera.playfieldRect().width / bg.sprite.rect.width*100f;
        bg.gameObject.transform.localScale = new Vector3(littleS, littleS, 1f);
        _sockCardPrefab = Resources.Load("prefabs/SockCardPrefab");
        var ssp1 = _sockCardPrefab.GetComponent<SockCardPrefabScript>();
        List<int> cardTypeList;
        if (thisLevel.CardTypes > 0)
        {
            var ctl = GenerateCardList(thisLevel.CardTypes,thisLevel.CardTypes);
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
            cardTypeList = GenerateCardList(ssp1.socks.Count,thisLevel.CardTypes);
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
            _tweenHolder.newTween(0.5f, alpha =>
            {
                var zTheta = (float)Math.Sin(2 * Math.PI * (1f-alpha))*15f*alpha;
                var t = sc.gameObject.transform;
                var rot = t.rotation;
                t.rotation = Quaternion.Euler(rot.eulerAngles.x,rot.eulerAngles.y,zTheta);
            },endAction: () =>
            {
                var t = sc.gameObject.transform;
                var rot = t.rotation;
                t.rotation = Quaternion.Euler(rot.eulerAngles.x,rot.eulerAngles.y,0);
            });
            _sockCardPrefabs.Add(sc);
        }
        
        
        var pfr = _mainCamera.playfieldRect();
        var left = pfr.xMin;
        var bottom = pfr.yMax;
        var tw = topWater.size;
        var bw = bottomWater.size;

        topWater.gameObject.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, 0f);
        bottomWater.gameObject.transform.position = new Vector3(left + bw.x / 2f, pfr.yMin - bw.y / 2f, 0f);
        
        
        
        MoveNo = thisLevel.Moves;

        
        
    }


    private List<int> GenerateCardList(int totalSocksCount, int sockNeeded)
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
                _gameState = GameState.Standby;
                _tweenHolder.newTween(0.5f, alpha =>
                {
                    var t = sockCardPrefabScript.gameObject.transform;
                    var x = (float)Math.Sin(alpha * Math.PI)*90;
                    t.rotation = Quaternion.Euler(t.eulerAngles.x,x,t.eulerAngles.z);
                    if (alpha > 0.5f)
                    {
                        if (!sockCardPrefabScript.sockVisible)
                        {
                            sockCardPrefabScript.SockVisible(true);       
                        }
                    }
                },endAction: () =>
                {
                    sockCardPrefabScript.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    for (var i = 0; i < _selection.Length; i++)
                    {
                        if (_selection[i] == -1)
                        {
                            _selection[i] = counter;
                            break;
                        }
                    }
                    _gameState = GameState.Game;
                });
            }
            if (broker)
            {
                break;
            }
        }
    }
    
    
    protected override void QuickSettingsButtonFunction()
    {
        _quickSettings.SetVisible(false);
        _gameState = GameState.Game;
    }
    
    protected override (int number, string text)  GetLevelPoints()
    {
        return (50 + MoveNo * 10, $"  {50 + MoveNo * 10}");
    }

    void Update()
    {
        _timer.Update(Time.deltaTime);
        _tweenHolder.Update(Time.deltaTime);

        if (_gameState==GameState.Game)
        {
            if (_sockCardPrefabs.Count == 0)
            {
                _gameState = GameState.Won;
                LevelDone(true);
            }else if (MoveNo <= 0)
            {
                _gameState = GameState.Lost;
                LevelDone(false);
            }
            
            HandleTouch();
            //Debug.Log($" {_selection[0]}, {_selection[1]}");
            if (_selection[_selection.Length-1] != -1)
            {
                _gameState = GameState.Standby;
                var endLoop = false;
                for (int i = 0; i < _selection.Length; i++)
                {
                    for (int j = i+1; j < _selection.Length; j++)
                    {
                        var firstType = _sockCardPrefabs[_selection[i]];
                        var secondType = _sockCardPrefabs[_selection[j]];

                        if (firstType.SelectedSockCard == secondType.SelectedSockCard)
                        {
                            _timer.addEvent(0.1f, () =>
                            {
                                _tweenHolder.newTween(0.3f, alpha =>
                                {
                                    var v = -alpha * alpha + 1;
                                    firstType.gameObject.transform.localScale = new Vector3(v,v,firstType.gameObject.transform.localScale.z);
                                    secondType.gameObject.transform.localScale = new Vector3(v,v,secondType.gameObject.transform.localScale.z);
                                }, endAction: () =>
                                {
                                    firstType.ToBeDestroyed = true;
                                    secondType.ToBeDestroyed = true;
                                });
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
                    MoveNo -= 1;
                    foreach (var sockCardPrefabScript in _sockCardPrefabs)
                    {
                        sockCardPrefabScript.SockVisible(false);
                    }
                    _gameState = GameState.Game;
                    foreach (var sockCardPrefabScript in _sockCardPrefabs)
                    {
                        if (sockCardPrefabScript.ToBeDestroyed)
                        {
                            Destroy(sockCardPrefabScript.gameObject);
                        }
                    }
                    _sockCardPrefabs.RemoveAll(x => x.ToBeDestroyed);
                });

                for (var i = 0; i < _selection.Length; i++)
                {
                    _selection[i] = -1;
                }
            }
        
        
            foreach (var sockCardPrefabScript in _sockCardPrefabs)
            {
                if (sockCardPrefabScript.ToBeDestroyed)
                {
                    Destroy(sockCardPrefabScript.gameObject);
                }
            }
            _sockCardPrefabs.RemoveAll(x => x.ToBeDestroyed);
        }else if (_gameState == GameState.Settings)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.SetVisible(true);
            }
        }else if (_gameState == GameState.Standby)
        {
            
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
