using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> dotSprites;

    public float width = 100f;
    public float height = 100f;
    
    
    public bool InTheRightPlace { get; set; } = false;
    public int Column { get; private set; } = -1;
    public int TargetRow = -1;
    public int Row{ get; private set; } = -1;

    public int DotType { get; private set; } = -1;
    
    
    public void setColumn(int c)
    {
        Column = c;
    }
    
    public void setRow(int r)
    {
        Row = r;
    }

    public void SetInfo(int? dt, float scaledSize, Vector2 gridCentre)
    {
        
        transform.position = new Vector3(gridCentre.x, gridCentre.y, -1f);

        var coeffW = scaledSize / (width / 100f);
        var coeffH = scaledSize / (height / 100f);
        //t.SetDotType();
        transform.localScale = new Vector3(coeffW, coeffH, 1f);
        if (dt != null)
        {
            SetDotType((int)dt);
        }
        
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
