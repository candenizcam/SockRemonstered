using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class DotsPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> dotSprites;

    private float width = 1f;// in world coordinates
    public float height = 1f; // in world coordinates
    private float _hitboxCoeff = 0.8f;
    private Rect _hitbox;
    
    public bool InTheRightPlace { get; set; } = false;
    public int Column { get; private set; } = -1;
    public int TargetRow = -1;
    public int Row{ get; private set; } = -1;

    public int DotType { get; private set; } = -1;


    public void TweenEffect(float scale)
    {
        
    }
    
    public void setColumn(int c)
    {
        Column = c;
    }
    
    public void setRow(int r)
    {
        Row = r;
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
        }

        _hitbox = RectTools.ScaleByCentre(gridRect, _hitboxCoeff, _hitboxCoeff);
    }

    public bool ContainsPoint(Vector2 p)
    {
        return _hitbox.Contains(p);
    }
    
    
    
    
    public void SetDotType(int dt)
    {
        DotType = dt;
        for (var i = 0; i < dotSprites.Count; i++)
        {
            dotSprites[i].enabled = i == dt;
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
