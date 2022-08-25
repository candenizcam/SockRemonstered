using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D;
using Random = System.Random;

public class SockPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> sockSpriteList;
    public Collider2D hitbox;
    public SpriteShapeRenderer hitboxRenderer;

    public float fallSpeed = 1f;
    // Start is called before the first frame update
    public bool ToBeDestroyed = false;

    public byte style = 0;
    public byte no = 0;

    private int _selectedSprite;
    void Awake()
    {
        hitboxRenderer.color = Color.clear;

        var r = new Random();

        _selectedSprite = r.Next(sockSpriteList.Count);
        for (int i = 0; i < sockSpriteList.Count; i++)
        {
            sockSpriteList[i].gameObject.SetActive(i == _selectedSprite);
        }

    }

    public void ChangeSprite(int cs)
    {
        _selectedSprite = cs;
        for (int i = 0; i < sockSpriteList.Count; i++)
        {
            sockSpriteList[i].gameObject.SetActive(i == _selectedSprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Collides(Vector2 point)
    {
        return hitbox.OverlapPoint(point);
    }

    public void MoveDownTime()
    {
        var y= gameObject.transform.position.y - Time.deltaTime * fallSpeed;
        Tools.MutatePosition(gameObject, y:y);
    }


}
