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
    public bool ThereIsMonster=true;
    void Awake()
    {
        
        try
        {
            YesMonster.enabled = false;
        }
        catch (UnassignedReferenceException e)
        {
            ThereIsMonster = false;
        }
        
        
        
        

    }

    public void MonsterEnabled(bool b)
    {
        Debug.LogWarning($"id: {ID}, b: {b}");
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
