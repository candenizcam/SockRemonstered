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
    
    private List<DotsPrefabScript> _dotsList = new List<DotsPrefabScript>();
    private SelectionList _selectionList = new SelectionList();
    private DotsScoreboard _dotsScoreboard;
    private DotGrid _dotGrid;
    //private DotsGameState _gameState = DotsGameState.StandBy;
    private DotsLevelsInfo _dotsLevelsInfo;
    private DotsHud _dotsHud => (DotsHud)_gameHud;
    
    void Awake()
    {
        _gameState = GameState.Standby;
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
        for (int i = 0; i < amn; i++)
        {
            _dotsList.Add(Instantiate(r).GetComponent<DotsPrefabScript>());
        }


        
        fillPhase();
        
        _tweenHolder.newTween(0.3f, alpha =>
        {
            var a = (float)Math.Sin(alpha * Math.PI * 2)*.2f;
            foreach (var selectionListSelection in _selectionList.Selections)
            {
                selectionListSelection.TweenEffect(a+1f);
            }
        },repeat:-1);


        InitializeUi<DotsHud>(_mainCamera);
        
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
        topFrame.gameObject.transform.position = new Vector3(left + tw.x / 2f, bottom + tw.y / 2f, 0f);
        
        var littleSs = w / (sockBg.bounds.size.x-.2f);
        sockBg.gameObject.transform.localScale = new Vector3(littleSs, littleSs, 1f);
        
        
        
        
        _dotsHud.UpdateTargets(_dotsScoreboard.GetRems());
        
        
    }

    protected override void QuickSettingsButtonFunction()
    {
        _quickSettings.setVisible(false);
        _gameState = GameState.Game;
    }
    
    void moveDown()
    {
        var realDots = _dotsList.Where(x => x.InTheRightPlace);
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
                }
                else if(!s.Any())
                {
                    delta += 1;
                }
            }
        }
        
        foreach (var dotsPrefabScript in _dotsList)
        {
            if (dotsPrefabScript.TargetRow != -1)
            {
                dotsPrefabScript.setRow(dotsPrefabScript.TargetRow);
                var gridRect =  _mainCamera.GetGridRect(dotsPrefabScript.Row,dotsPrefabScript.Column);
                dotsPrefabScript.SetInfo(null, _mainCamera.ScaledSingleSize,gridRect);
                dotsPrefabScript.TargetRow = -1;
            }
        }
    }
    
    


    void fillPhase()
    {
        var cn = ColNeeds();
        var s = "";
        foreach (var i in cn)
        {
            s += $"{i},";
        }
        fillGrid(cn);
    }

    void fillGrid(int[] colNeeds)
    {
        var unstable = _dotsList.Where(x => !x.InTheRightPlace).ToList();

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
                t.gameObject.SetActive(true);
                t.InTheRightPlace = true;
                unstable.Remove(t);
                colNeeds[j] -= 1;

            }
        }
        
        
    }


    int[] ColNeeds()
    {
        var stabilized = _dotsList.Where(x => x.InTheRightPlace);
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
            _dotsScoreboard.AddToRemoved(_selectionList.getTypes());
            
            foreach (var dotsPrefabScript in _selectionList.Selections)
            {
                
                dotsPrefabScript.InTheRightPlace = false;
                dotsPrefabScript.gameObject.SetActive(false);
                
            }
            
            
            moveDown();
        
            fillPhase();
            _dotsHud.UpdateTargets(_dotsScoreboard.GetRems());
            var hi = _dotsScoreboard.GetHudInfo();
            _dotsHud.updateInfo(hi.movesLeft,hi.mood);
            
        }
        
        
        _selectionList.Clear();
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

        
        
        _selectionList.SetDragTip(touchWp, _mainCamera.Scale);
        
        var p = _mainCamera.WorldToGridPos(touchWp);

        if (p.r == -1)
        {
            return;
        }
        
        var thisDot = _dotsList.Find(x => x.Row == p.r && x.Column == p.c);

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
            _selectionList.SetDragChain( _mainCamera.Scale);
        }
        else
        {
            if (lineType == thisDot.DotType)
            {
                if (_selectionList.IsAdjacent(thisDot) || _selectionList.ListContains(thisDot))
                {
                    _selectionList.AddDot(thisDot);
                    _selectionList.SetDragChain( _mainCamera.Scale);
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
        _dotsHud.Update();
        _betweenLevels.Update();
        _quickSettings.Update();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateEngine();
        
        
        if (_gameState == GameState.Game)
        {
            HandleTouch();

            if (_dotsScoreboard.GameWon())
            {
                _gameState = GameState.Won;
                LevelDone(true);
            }else if (_dotsScoreboard.GameLost())
            {
                _gameState = GameState.Lost;
                LevelDone(false);
            }
            
            
        }else if (_gameState == GameState.Settings)
        {
            if (!_quickSettings.Active)
            {
                _quickSettings.setVisible(true);
            }
        }else if (_gameState == GameState.Standby)
        {
            if (_dotsList.All(x => x.InTheRightPlace))
            {
                _gameState = GameState.Game;
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
