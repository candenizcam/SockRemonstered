using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards.CScripts;
using Unity.VisualScripting;
using UnityEngine;

public class CardsMain : MonoBehaviour
{
    
    private System.Random _random;
    private CardLayout _mainCamera;

    private int _columns;
    private int _rows;

    private UnityEngine.Object _sockCardPrefab;
    private List<SockCardPrefabScript> _sockCardPrefabs = new List<SockCardPrefabScript>();
    private int[] _selection;
    void Awake()
    {
        _random = new System.Random();        
        
        _rows = 4;
        _columns = 3;
        _mainCamera = new CardLayout(Camera.main, _rows, _columns);
        _selection = new int[] {-1,-1};


        _sockCardPrefab = Resources.Load("prefabs/SockCardPrefab");
        var ssp1 = _sockCardPrefab.GetComponent<SockCardPrefabScript>();

        var cardTypeList = generateCardList(ssp1.socks.Count);
        
        
        
        for (var i = 0; i < cardTypeList.Count; i++)
        {
            
            var c = i % _columns;
            var r = i / _columns;
            var s = (GameObject)Instantiate(_sockCardPrefab);
            var sc = s.GetComponent<SockCardPrefabScript>();
            sc.SelectedSockCard = cardTypeList[i];
            sc.Resize(_mainCamera.Centres[r,c], _mainCamera.SingleScale);
            //s.transform.position = _mainCamera.Centres[r,c];
            //s.transform.localScale = _mainCamera.SingleScale;
            _sockCardPrefabs.Add(sc);
        }
        


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


    private List<int> generateCardList(int socksCount)
    {
        var totalCards =( _rows * _columns)/2;
        if ((_rows * _columns) % 2 != 0)
        {
            throw new Exception("invalid row col entry");
        }
        var fullTurns = totalCards / socksCount;
        var missing = totalCards % socksCount;
        var r = new List<int>();
        for (int j = 0; j <socksCount; j++)
        {
            r.Add(j);
        }
        var m = r.OrderBy(_ => _random.Next()).ToList();
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
        
        return m2;
    }

    private void HandleTouch()
    {
        var thisTurnTouches = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended).ToList();

        if (thisTurnTouches.Count <= 0) return;
        var firstTouch = thisTurnTouches[0];
        var counter = 0;
        var broker = false;
        foreach (var sockCardPrefabScript in _sockCardPrefabs)
        {
            var worldPoint = _mainCamera.Camera.ScreenToWorldPoint(firstTouch.position);
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
            
            counter += 1;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleTouch();

        //Debug.Log($" {_selection[0]}, {_selection[1]}");
        if (_selection[_selection.Length-1] != -1)
        {
            //_selection[0] = -1;
            //_selection[1] = -1;
            
            var broker = false;
            for (int i = 0; i < _selection.Length; i++)
            {
                for (int j = i+1; j < _selection.Length; j++)
                {
                    if (_sockCardPrefabs[_selection[i]].SelectedSockCard ==
                        _sockCardPrefabs[_selection[j]].SelectedSockCard)
                    {
                        Debug.Log($"i: {i}, j: {j}");
                        _sockCardPrefabs[_selection[i]].DestroyThis();
                        _sockCardPrefabs[_selection[j]].DestroyThis();
                        broker = true;
                        break;
                    }
                    
                }

                if (broker)
                {
                    break;
                }
            }

            for (var i = 0; i < _selection.Length; i++)
            {

                if (!_sockCardPrefabs[_selection[i]].ToBeDestroyed)
                {
                    _sockCardPrefabs[_selection[i]].SockVisible(false);
                        
                }
                _selection[i] = -1;
            }
            
            _sockCardPrefabs.RemoveAll(x => x.ToBeDestroyed);
            foreach (var sockCardPrefabScript in _sockCardPrefabs)
            {
                sockCardPrefabScript.SockVisible(false);
            }
            
        }

        

    }
}
