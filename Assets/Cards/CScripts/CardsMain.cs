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
    
    void Awake()
    {
        _random = new System.Random();        
        
        _mainCamera = new CardLayout(Camera.main, _rows, _columns);

        _rows = 4;
        _columns = 6;


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
            //s.transform.position = new Vector3(, 3f);
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
        
        for (var i = 0; i < r2.Count; i++)
        {
            r2.Add(r2[i]);
        }
        var m2 = r2.OrderBy(_ => _random.Next()).ToList();
        
        return m2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
