using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SpriteRenderer> ObstacleVisuals;

    public int Row;
    public int Column;
    private int ObstacleIndex = 0;


    public float WorldWidth => ObstacleVisuals[ObstacleIndex].bounds.size.x;
    public float WorldHeight => ObstacleVisuals[ObstacleIndex].bounds.size.y;

    public void SetObstacleIndex(int obstacleNo)
    {
        ObstacleIndex = obstacleNo - 1;
        for (var i = 0; i < ObstacleVisuals.Count; i++)
        {
            ObstacleVisuals[i].enabled = i == ObstacleIndex;
        }
    }
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
