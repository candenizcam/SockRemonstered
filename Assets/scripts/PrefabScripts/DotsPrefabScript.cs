using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Unity.Mathematics;
using UnityEngine;

public class DotsPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> dotSprites;
    public List<SpriteRenderer> bombSprites;
    private List<SpriteRenderer> allSprites;
    public SpriteRenderer HitBlob;
    public SpriteRenderer DragBar;
    
    private float width = 1f;// in world coordinates
    public float height = 1f; // in world coordinates
    private float _hitboxCoeff = 0.8f;
    private Rect _hitbox;
    private float _scale = 1f;
    
    public bool InTheRightPlace { get; set; } = false;
    public int Column { get; private set; } = -1;
    public int TargetRow = -1;
    
    public int Row{ get; private set; } = -1;

    public int DotType { get; private set; } = -1;
    
    [NonSerialized]
    public int? TurnInto = null;

    public bool ImTheBomb => DotType >= dotSprites.Count;  // 3,2,1
    public int BombType => DotType - dotSprites.Count; // 0: adj.


    public void DeathEffect(float alpha)
    {
        var a = -1f * alpha * alpha + 1;
        allSprites[DotType].transform.localScale = new Vector3(a, a, 1f);
        allSprites[DotType].transform.rotation = Quaternion.Euler(0f,0f,alpha*720f);
    }
    
    public void TweenEffect(float scale)
    {
        allSprites[DotType].gameObject.transform.localScale = new Vector3(scale, scale, 1f);
    }
    
    public void setColumn(int c)
    {
        Column = c;
    }
    
    public void setRow(int r)
    {
        Row = r;
    }


    
    
    public void SetPosition(float? x = null, float? y = null, float? z = null)
    {
        var p = transform.position;
        transform.position = new Vector3(x ??= p.x, y ??= p.y, z??=-1f);
    }
    
    public void SetInfo(int? dt, float scaledSize, Rect gridRect)
    {
        
        transform.position = new Vector3(gridRect.center.x, gridRect.center.y, -1f);

        var coeffW = scaledSize / (width);
        var coeffH = scaledSize / (height);
        transform.localScale = new Vector3(coeffW, coeffH, 1f);
        if (dt != null)
        {
            SetDotType((int)dt);
            HitBlob.color = Constants.DotsColours[(int)dt];
            DragBar.color = Constants.DotsColours[(int)dt];
        }

        _hitbox = RectTools.ScaleByCentre(gridRect, _hitboxCoeff, _hitboxCoeff);
        
        
        var hbs = scaledSize * _hitboxCoeff/coeffW;
        HitBlob.gameObject.transform.localScale = new Vector3(hbs, hbs, 1f);
        DragBar.gameObject.transform.localScale = new Vector3(hbs, hbs*0.25f, 1f);
    }

    public bool ContainsPoint(Vector2 p)
    {
        return _hitbox.Contains(p);
    }

    public void SqueezeDragBar()
    {
        var dgt = DragBar.gameObject.transform;
        dgt.localScale = new Vector3(.01f, dgt.localScale.y, dgt.localScale.z);
    }

    public void MoveDragBar(Vector2 target, Color? forceColour = null)
    {
        var p1 = transform.position;
        var c = (Vector2)VectorTools.Vector3BiasedSum(target, p1, 0.5f);

        var dgt = DragBar.gameObject.transform;
        dgt.position = new Vector3(c.x, c.y,2);

        var m = ((Vector2) VectorTools.Vector3Add(target, -1 * p1));
        var v = (Vector2)VectorTools.vector3Div(m, (Vector2)transform.localScale);
        dgt.localScale = new Vector3(v.magnitude, dgt.localScale.y, dgt.localScale.z);
        var ang = (float)Math.Atan2(m.x, m.y);
        
        dgt.rotation = Quaternion.Euler(0f,0f,-ang / 6.282f * 360f+90f);

        if (forceColour != null)
        {
            DragBar.color = (Color)forceColour;
        }

    }
    
    
    
    
    public void SetDotType(int dt)
    {
        TurnInto = null;

        if (dt >= 0)
        {
            DotType = dt;
        }
        else
        {
            DotType = dotSprites.Count - dt - 1;
        }
        
        for (var i = 0; i < allSprites.Count; i++)
        {
            allSprites[i].enabled = i == DotType;
        }
        
        /*
        DotType = dt;
        if (dt >= 0)
        {
            for (var i = 0; i < allSprites.Count; i++)
            {
                allSprites[i].enabled = i == dt;
            }

            /*
            for (var i = 0; i < dotSprites.Count; i++)
            {
                dotSprites[i].enabled = i == dt;
            }
            foreach (var spriteRenderer in bombSprites)
            {
                spriteRenderer.gameObject.SetActive(false);
            }
            
        }
        else
        {
            
            for (var i = 0; i < allSprites.Count; i++)
            {
                allSprites[i].enabled = i == dt;
            }
            /*
            foreach (var spriteRenderer in dotSprites)
            {
                spriteRenderer.enabled = false;
            }
            
            for (var i = 0; i < bombSprites.Count; i++)
            {
                bombSprites[i].enabled = i == Math.Abs(dt) - 1;
            }
            
            
            
        }
        */
        
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        HitBlob.gameObject.SetActive(false);
        DragBar.gameObject.SetActive(false);

        allSprites = new List<SpriteRenderer>();
        
        foreach (var spriteRenderer in dotSprites)
        {
            allSprites.Add(spriteRenderer);
        }
        
        foreach (var spriteRenderer in bombSprites)
        {
            allSprites.Add(spriteRenderer);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
