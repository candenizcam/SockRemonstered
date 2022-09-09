using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    public SpriteRenderer NoMonster;
    public SpriteRenderer YesMonster;

    public string ID;
    // Start is called before the first frame update
    [NonSerialized]
    public bool ThereIsMonster=false;
    void Awake()
    {
        
        try
        {
            YesMonster.enabled = false;
            ThereIsMonster = true;
        }
        catch (UnassignedReferenceException e)
        {
            ThereIsMonster = false;
        }
        
        
        
        

    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void MonsterEnabled(bool b)
    {
        
        if (ThereIsMonster)
        {
            NoMonster.enabled = !b;
            YesMonster.enabled = b;
        }
        else
        {
            Debug.LogWarning("you fucked up, there is no monster");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
