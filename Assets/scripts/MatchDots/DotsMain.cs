using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.CScripts;
using Classes;
using MatchDots;
using UnityEngine;
using UnityEngine.UIElements;

public class DotsMain : GameMain
{
    public SpriteRenderer bg;
    //public SpriteRenderer BottomFrame;
    public SpriteRenderer topFrame;
    public SpriteRenderer sockBg;
    // Start is called before the first frame update
    public int LevelNo;
    private int _levelNo;
    private System.Random _lineRandom;
    private DotsLayout _mainCamera;
    private int _rows;
    private int _cols;
    private SelectionList _selectionList = new SelectionList();
    private DotsScoreboard _dotsScoreboard;
    private DotGrid _dotGrid;
    //private DotsGameState _gameState = DotsGameState.StandBy;
    private DotsLevelsInfo _dotsLevelsInfo;
    private DotsHud _dotsHud => (DotsHud)_gameHud;
    
    private const float EraseTime = 0.5f;
    private const float FallTime = 0.8f;
    private const float SmallFallTime = 0.2f;
    
    void Awake()
    {
        _gameState = GameState.Loading;
        var sgd = SerialGameData.LoadOrGenerate();
        if (LevelNo > 0)
        {
            _levelNo = LevelNo - 1;
        }
        else
        {
            var levelInfo = Constants.GetNextLevel(sgd.nextLevel);

            if (levelInfo.SceneName != "Dots")
            {
                throw new Exception("there is a problem");
            }
        
            _levelNo = levelInfo.LevelNo;
        }

        _tweenHolder = new TweenHolder();
        _random = new System.Random();
        
        var thisLevel = DotsLevels.Levels[_levelNo];
        _rows = thisLevel.Rows;
        _cols = thisLevel.Cols;
        _dotsLevelsInfo = thisLevel;
        _lineRandom = new System.Random(thisLevel.Seeds[0]);
        _dotsScoreboard = new DotsScoreboard(thisLevel);
        
        //Screen.
        _mainCamera = new DotsLayout(Camera.main, _rows, _cols);
        InitializeMisc();
        
        
        _dotGrid = new DotGrid(thisLevel);
        var r = Resources.Load<GameObject>("prefabs/DotsPrefab");
        var amn = _dotGrid.ActiveMemberNo();
        var dl = new List<DotsPrefabScript>();
        for (int i = 0; i < amn; i++)
        {
            dl.Add(Instantiate(r).GetComponent<DotsPrefabScript>());
            dl.Last().gameObject.SetActive(false);
        }
        _dotGrid.FillTheDotsList(dl);

        


        
        
        


        InitializeUi<DotsHud>(tutorialFrames: DotsLevels.Tutorial);
        
        //_dotsScoreboard.MoveCounter
        
        //_dotsHud.updateInfo($"{_levelMoves}",MonsterMood.Happy);
        var hi = _dotsScoreboard.GetHudInfo();
        _dotsHud.updateInfo(hi.movesLeft,hi.mood);
        
        var w = _mainCamera.Camera.orthographicSize*_mainCamera.Camera.aspect*2f;
        
        var littleS = w / (bg.bounds.size.x-.5f);
        
        bg.gameObject.transform.localScale = new Vector3(littleS, littleS, 1f);
        
        var pfr = _mainCamera.playfieldRect();
        var left = pfr.xMin;
        var bottom = pfr.yMax;
        var tw = topFrame.size;
        topFrame.gameObject.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, -3f);
        
        var littleSs = w / (sockBg.bounds.size.x-.2f);
        sockBg.gameObject.transform.localScale = new Vector3(littleSs, littleSs, 1f);
        
        
        
        
        _dotsHud.UpdateTargets(_dotsScoreboard.GetRems());
        
        _dotsHud.StartAnimation(0f);
        _timer.addEvent(0.75f, () =>
        {
            _tweenHolder.newTween(.75f, alpha =>
            {
                _dotsHud.StartAnimation(Math.Clamp(alpha*1.2f,0f,1f));
            }, () =>
            {
                _dotsHud.StartAnimation(1f);
                _gameState = GameState.Standby;
                _dotsHud.ClearBg();

                _timer.addEvent(0.3f, () =>
                {
                    
                    fillPhase();
                    _tweenHolder.newTween(0.3f, alpha =>
                    {
                        var a = (float)Math.Sin(alpha * Math.PI * 2)*.2f;
                        foreach (var selectionListSelection in _selectionList.Selections.Where(selectionListSelection => selectionListSelection.InTheRightPlace))
                        {
                            selectionListSelection.TweenEffect(a+1f);
                        }
                    },repeat:-1);
                });
                
            });
        });
        
        
        
        
    }


    
    void moveDown()
    {
        var realDots = _dotGrid.DotsList.Where(x => x.InTheRightPlace);
        for (int i = 0; i < _cols; i++)
        {
            var c = i + 1;

            var thisCol = realDots.Where(x => x.Column == c);
            if(!thisCol.Any()) continue;
            var delta = 0;
            for (int j = _rows; j > 0; j--)
            {
                
                if(_dotGrid.GapSpot(j,c)) continue;

                var s = thisCol.Where(x => x.Row == j);

                if (s.Any() && delta>0)
                {
                    var f = s.First();
                    f.TargetRow =  f.Row + delta;
                    f.InTheRightPlace = false;
                }
                else if(!s.Any())
                {
                    delta += 1;
                }
            }
        }

        var movers = _dotGrid.DotsList.Where(x => x.TargetRow != -1);
        if(!movers.Any()) return;

        
        var maxDRow = movers.Max(x => x.TargetRow - x.Row);
        
        foreach (var dotsPrefabScript in movers)
        {
            var oldY = dotsPrefabScript.gameObject.transform.position.y;
            var dRow = dotsPrefabScript.TargetRow - dotsPrefabScript.Row;
                
            dotsPrefabScript.setRow(dotsPrefabScript.TargetRow);
                
            var gridRect =  _mainCamera.GetGridRect(dotsPrefabScript.Row,dotsPrefabScript.Column);
            var newY = gridRect.center.y;
                
            dotsPrefabScript.SetInfo(null, _mainCamera.ScaledSingleSize,gridRect);
            dotsPrefabScript.SetPosition(y: oldY);
            _tweenHolder.newTween(SmallFallTime/maxDRow*dRow, alpha =>
            {
                dotsPrefabScript.SetPosition(y: oldY*(1f-alpha) + alpha*newY);
            }, () =>
            {
                dotsPrefabScript.InTheRightPlace = true;
                dotsPrefabScript.SetPosition(y: newY);
            });
            dotsPrefabScript.TargetRow = -1;
        }
        
        
    }
    
    


    void fillPhase()
    {
        var cn = ColNeeds();
        fillGrid(cn);
    }

    void fillGrid(int[] colNeeds)
    {
        var unstable = _dotGrid.DotsList.Where(x => !x.InTheRightPlace).ToList();

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                
                if (colNeeds[j]==0) continue;

                var t = unstable[0];
                var r = _dotGrid.GetRowFromColTop(j+1, colNeeds[j]);
                
                var gridRect = _mainCamera.GetGridRect(r,j+1);
                t.setRow(r);
                t.setColumn(j+1);
                t.SetInfo(_lineRandom.Next(0, _dotsLevelsInfo.DotTypes), _mainCamera.ScaledSingleSize,gridRect);
                var y = gridRect.center.y;
                t.SetPosition(y : y + (float)2f*_mainCamera.Camera.orthographicSize);
                t.gameObject.SetActive(true);
                _tweenHolder.newTween(.8f, alpha =>
                {
                    var dy = 2f*_mainCamera.Camera.orthographicSize*Math.Abs(Math.Cos(8f*alpha))*Math.Exp(-4f*alpha);
                    t.SetPosition(y : y + (float)dy);
                }, () =>
                {
                    t.SetPosition(y : y);
                    t.InTheRightPlace = true;
                });
                unstable.Remove(t);
                colNeeds[j] -= 1;

            }
        }
        
        
    }


    int[] ColNeeds()
    {
        var stabilized = _dotGrid.DotsList.Where(x => x.InTheRightPlace);
        var a = new int[_cols];
        for (int i = 0; i < _cols; i++)
        {
            a[i] = _rows - stabilized.Count(x => x.Column == i+1);
        }
        return a;
    }


    void TerminateTouch()
    {
        //_movesLeft -= 1;
        if (_selectionList.Selections.Count > 2)
        {
            _gameState = GameState.Standby;
            _dotsScoreboard.AddToRemoved(_selectionList.getTypes());

            foreach (var dotsPrefabScript in _selectionList.Selections)
            {
                dotsPrefabScript.InTheRightPlace = false;
            }

            if (_selectionList.Selections.Count > Constants.DotsAdjBombNumber)
            {
                _selectionList.Selections.Last().TurnInto = -1;
            }
            _selectionList.ClearTouchEffects();
            _tweenHolder.newTween(EraseTime, alpha =>
            {
                //var a = -1f * alpha * alpha + 1;
                foreach (var dotsPrefabScript in _selectionList.Selections)
                {
                    dotsPrefabScript.DeathEffect(alpha);
                    //dotsPrefabScript.gameObject.transform.localScale = new Vector3(a, a, 1f);
                    //dotsPrefabScript.gameObject.transform.rotation = Quaternion.Euler(0f,0f,alpha*720f);
                }
                
            }, () =>
            {
                foreach (var dotsPrefabScript in _selectionList.Selections)
                {
                    if (dotsPrefabScript.TurnInto == null)
                    {
                        dotsPrefabScript.gameObject.SetActive(false);
                    }
                    else
                    {
                        //Debug.Log("hayde bude loco loco");
                        dotsPrefabScript.InTheRightPlace = true;
                        dotsPrefabScript.DeathEffect(0f);
                        //dotsPrefabScript.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        //dotsPrefabScript.gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
                        dotsPrefabScript.SetDotType((int)dotsPrefabScript.TurnInto);
                    }
                    
                }
                
            });
            _timer.addEvent(EraseTime+.1f, () =>
            {
                _selectionList.Clear();
                moveDown();
            });
        
            _timer.addEvent(EraseTime+SmallFallTime+.2f, () =>
            {
                fillPhase();
                _dotsHud.UpdateTargets(_dotsScoreboard.GetRems());
                var hi = _dotsScoreboard.GetHudInfo();
                _dotsHud.updateInfo(hi.movesLeft,hi.mood);
                
                if (!_dotGrid.AnyLegalMoves())
                {
                    NoLegalMoves();
                }
            });    
        }
        else
        {
            _selectionList.Clear();
        }
        
        
        
    }
    
    private void HandleTouch()
    {
        if (Input.touches.Length == 0)
        {
            return;
        }
        
        var thisTurnEnders = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended).ToList();


        Touch thisTouch;
        try
        {
            thisTouch = Input.touches.First(x => x.phase != TouchPhase.Ended);
        }
        catch (InvalidOperationException)
        {
            TerminateTouch();
            return;
        }


        var touchWp = _mainCamera.Camera.ScreenToWorldPoint(thisTouch.position);

        _selectionList.SetDragTip(touchWp);
        
        var p = _mainCamera.WorldToGridPos(touchWp);

        if (p.r == -1)
        {
            return;
        }
        
        var thisDot = _dotGrid.DotsList.Find(x => x.Row == p.r && x.Column == p.c);

        if (_selectionList.IsLatestPick(thisDot))
        {
            return;
        }

        if(!thisDot.ContainsPoint(touchWp))
        {
            return;
        }

        var lineType = _selectionList.LineType();

        if (lineType == -1)
        {
            _selectionList.AddDot(thisDot);
            _selectionList.SetDragChain();
        }
        else
        {
            if (lineType == thisDot.DotType)
            {
                if (_selectionList.IsAdjacent(thisDot) || _selectionList.ListContains(thisDot))
                {
                    _selectionList.AddDot(thisDot);
                    _selectionList.SetDragChain();
                }
            }
        }
    }
    
    protected override (int number, string text)  GetLevelPoints()
    {
        return _dotsScoreboard.GetLevelPoints();
    }
    
    
    void UpdateEngine()
    {
        _tweenHolder.Update(Time.deltaTime);
        _timer.Update(Time.deltaTime);
    }


    void Explode()
    {
        var bombs = _dotGrid.DotsList.Where(x => x.ImTheBomb);
        
        foreach (var bomb in bombs)
        {
            bomb.InTheRightPlace = false;
            var type = bomb.BombType;
            var l = new List<DotsPrefabScript>();
            if (type == 0)
            { //adj
                for (var i = -1; i < 2; i++)
                {
                    var row = _dotGrid.DotsList.Where(x => x.Row == bomb.Row + i);
                    if(!row.Any()) continue;
                    for (var j = -1; j < 2; j++)
                    {
                        var c = row.Where(x => x.Column == bomb.Column + j);
                        if(!c.Any()) continue;

                        foreach (var dotsPrefabScript in c)
                        {
                            l.Add(dotsPrefabScript);
                        }
                    }
                }
                
            }
            
            _dotsScoreboard.AddToRemoved(l.Select(x => x.DotType).ToList());
            
            foreach (var dotsPrefabScript in l)
            {
                dotsPrefabScript.InTheRightPlace = false;
            }
            
            foreach (var dotsPrefabScript in l)
            {
                dotsPrefabScript.HitBlob.gameObject.SetActive(true);
                dotsPrefabScript.HitBlob.color = Color.black;
                if(bomb==dotsPrefabScript) continue;
                
                dotsPrefabScript.MoveDragBar(bomb.gameObject.transform.position, Color.black);
                
            
                
                dotsPrefabScript.DragBar.gameObject.SetActive(true);
                
            }
            
            
            _tweenHolder.newTween(EraseTime, alpha =>
            {
                var a = -1f * alpha * alpha + 1;
                foreach (var dotsPrefabScript in l)
                {
                    dotsPrefabScript.DeathEffect(alpha);
                    //dotsPrefabScript.gameObject.transform.localScale = new Vector3(a, a, 1f);
                    //dotsPrefabScript.gameObject.transform.rotation = Quaternion.Euler(0f,0f,alpha*720f);
                }
                
            }, () =>
            {
                foreach (var dotsPrefabScript in l)
                {
                    if (dotsPrefabScript.TurnInto == null)
                    {
                        dotsPrefabScript.gameObject.SetActive(false);
                    }
                    else
                    {
                        //Debug.Log("hayde bude loco loco");
                        dotsPrefabScript.InTheRightPlace = true;
                        dotsPrefabScript.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        dotsPrefabScript.gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
                        dotsPrefabScript.SetDotType((int)dotsPrefabScript.TurnInto);
                    }
                    
                }
                
                foreach (var dotsPrefabScript in l)
                {
                    dotsPrefabScript.TweenEffect(1f);
                    dotsPrefabScript.HitBlob.gameObject.SetActive(false);
                    dotsPrefabScript.DragBar.gameObject.SetActive(false);
                }
                
            });
            _timer.addEvent(EraseTime+.1f, () =>
            {
                
                moveDown();
            });
        
            _timer.addEvent(EraseTime+SmallFallTime+.2f, () =>
            {
                fillPhase();
                _dotsHud.UpdateTargets(_dotsScoreboard.GetRems());
                var hi = _dotsScoreboard.GetHudInfo();
                _dotsHud.updateInfo(hi.movesLeft,hi.mood);
                
                if (!_dotGrid.AnyLegalMoves())
                {
                    NoLegalMoves();
                }
            });  
            
            //foreach (var dotsPrefabScript in l)
            //{
            //    dotsPrefabScript.SetDotType(-1);
            //}
            //Debug.Log($"exploding: {l.Count}");
            
            
        }
    }

    private void NoLegalMoves()
    {
        _gameState = GameState.Lost;
        LevelDone(false);
        _betweenLevels.UpdateSmallText(  "No more moves!");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateEngine();
        
        
        if (_gameState == GameState.Game)
        {
            if (_dotsScoreboard.GameWon())
            {
                _gameState = GameState.Won;
                LevelDone(true);
            }else if (_dotsScoreboard.GameLost())
            {
                _gameState = GameState.Lost;
                LevelDone(false);
            }
            HandleTouch();
            
        }else if (_gameState == GameState.Settings)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.SetVisible(true);
            }
            _quickSettings.TutorialRoll(Time.deltaTime);
        }else if (_gameState == GameState.Standby)
        {
            if (_dotGrid.DotsList.All(x => x.InTheRightPlace))
            {
                if (_dotGrid.DotsList.Any(x => x.ImTheBomb))
                {
                
                    Explode();
                }
                else
                {
                    _gameState = GameState.Game;
                    
                }
                
                
            }
        }else if (_gameState == GameState.Won || _gameState == GameState.Lost)
        {
            if (!_betweenLevels.Active)
            {
                _betweenLevels.setVisible(true);
            }
        }
    }

    
}
