using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotstacleScript : MonoBehaviour
{
    
    public SpriteRenderer ObstacleVisual;
    public SpriteRenderer ObstacleBack;
    public int Row;
    public int Column;

    public float WorldWidth => ObstacleVisual.bounds.size.x;
    public float WorldHeight => ObstacleVisual.bounds.size.y;

    public void Recolour(Color c)
    {
        ObstacleBack.color = c;
    }

    
}
