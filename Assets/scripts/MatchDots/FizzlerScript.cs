using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzlerScript : MonoBehaviour
{
    public List<FizzlerColourScript> fizzlerColourScripts;

    public SpriteRenderer biggerSock;
    [NonSerialized] public int FizzlerColour = -1;
    [NonSerialized] public int FizzlerBrightness = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < fizzlerColourScripts.Count; i++)
        {
            fizzlerColourScripts[i].Intensity = FizzlerColour==i ? FizzlerBrightness: 0;
        }

    }
}
