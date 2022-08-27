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
    
    
    void Awake()
    {
        _random = new System.Random();        
        
        _mainCamera = new CardLayout(Camera.main);

        _rows = 4;
        _columns = 6;


        _sockCardPrefab = Resources.Load("prefabs/SockCardPrefab");
        var ssp1 = _sockCardPrefab.GetComponent<SockCardPrefabScript>();

        var cardList = generateCardList(ssp1.socks.Count);


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
        return r2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
