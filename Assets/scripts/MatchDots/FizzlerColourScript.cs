using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzlerColourScript : MonoBehaviour
{
    public List<SpriteRenderer> bright;
    public List<SpriteRenderer> brighter;
    public List<SpriteRenderer> brightest;
    private List<SpriteRenderer>[] brightAlliance;
    public float q;
    
    private float _phase = 0f;
    private readonly float _period = 1f;
    [NonSerialized] public int Intensity = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        brightAlliance = new []{bright,brighter,brightest};
        HideAll();
    }

    void HideAll()
    {
        foreach (var spriteRenderer in bright)
        {
            spriteRenderer.enabled = false;
        }
        foreach (var spriteRenderer in brighter)
        {
            spriteRenderer.enabled = false;
        }
        foreach (var spriteRenderer in brightest)
        {
            spriteRenderer.enabled = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        _phase += Time.deltaTime;

        if (_phase > _period)
        {
            _phase %= _period;
        }
        
        for (var i = 0; i < brightAlliance.Length; i++)
        {
            foreach (var spriteRenderer in brightAlliance[i])
            {
                spriteRenderer.enabled = Intensity == i + 1;
            }
            
            
            if (Intensity == i + 1)
            {
                var g = brightAlliance[i];
                var q = _phase * (g.Count - 1f);
                var alpha = (float)Math.Cos((q % 1f)*2*Math.PI)*0.5f+0.5f;
                this.q = q;
                for (var j = 0; j < brightAlliance[i].Count; j++)
                {
                    //g[i].enabled = true;
                    
                    if ((int) q == j) g[j].color = new Color(1f, 1f, 1f, alpha);
                    else if ((int) q + 1 == j) g[j].color = new Color(1f, 1f, 1f, 1f - alpha);
                    else g[j].enabled = false;
                }
            }
           
        }

        
        
        
    }
}
