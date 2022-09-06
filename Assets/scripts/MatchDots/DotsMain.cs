using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.CScripts;
using Classes;
using MatchDots;
using UnityEngine;
using UnityEngine.UIElements;

public class DotsMain : MonoBehaviour
{
    // Start is called before the first frame update
    public int LevelNo;
    private int _levelNo;
    private TweenHolder _tweenHolder;
    private System.Random _random;
    private System.Random _lineRandom;
    private DotsLayout _mainCamera;
    private UIDocument _uiDocument;
    private int _rows;
    private int _cols;
    private List<DotsPrefabScript> _dotsList = new List<DotsPrefabScript>();
    private DotGrid _dotGrid;
    private DotsGameState _gameState = DotsGameState.StuffMoves;
    private DotsLevelsInfo _dotsLevelsInfo;
    
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

        _tweenHolder = new TweenHolder();
        _random = new System.Random();
        
        var thisLevel = DotsLevels.Levels[_levelNo];
        _rows = thisLevel.Rows;
        _cols = thisLevel.Cols;
        _dotsLevelsInfo = thisLevel;
        _lineRandom = new System.Random(thisLevel.Seeds[0]);
        
        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        
        
        _mainCamera = new DotsLayout(Camera.main, _rows, _cols);
        _dotGrid = new DotGrid(thisLevel);


        var r = Resources.Load<GameObject>("prefabs/DotsPrefab");
        var amn = _dotGrid.ActiveMemberNo();
        for (int i = 0; i < amn; i++)
        {
            _dotsList.Add(Instantiate(r).GetComponent<DotsPrefabScript>());
        }


        
        fillPhase();

        var removeList = _dotsList.Where(x => x.DotType == 0);

        foreach (var dotsPrefabScript in removeList)
        {
            dotsPrefabScript.InTheRightPlace = false;
            dotsPrefabScript.gameObject.SetActive(false);
        }

        moveDown();
        
        fillPhase();
        
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
                Debug.Log("something");
                dotsPrefabScript.setRow(dotsPrefabScript.TargetRow);
                
                var gridRect =  _mainCamera.GetGridRect(dotsPrefabScript.Row,dotsPrefabScript.Column);
                dotsPrefabScript.SetInfo(null, _mainCamera.ScaledSingleSize,gridRect.center);
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
        Debug.Log(s);
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
                t.SetInfo(_lineRandom.Next(0, _dotsLevelsInfo.DotTypes), _mainCamera.ScaledSingleSize,gridRect.center);
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

    // Update is called once per frame
    void Update()
    {
        
    }



    enum DotsGameState
    {
        Loading, Game, StuffMoves, GameLost, GameWon 
    };
}
