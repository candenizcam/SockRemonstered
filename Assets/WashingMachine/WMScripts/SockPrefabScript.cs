using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D;

public class SockPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> sockSpriteList;
    public Collider2D hitbox;
    public SpriteShapeRenderer hitboxRenderer;
    public SpriteShapeController hitboxController;
    // Start is called before the first frame update
    public bool ToBeDestroyed = false;
    void Awake()
    {
        //hitboxRenderer.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Collides(Vector2 point)
    {
        return hitbox.OverlapPoint(point);
    }

}
