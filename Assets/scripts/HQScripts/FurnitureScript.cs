using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    public SpriteRenderer NoMonster;
    public SpriteRenderer YesMonster;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
