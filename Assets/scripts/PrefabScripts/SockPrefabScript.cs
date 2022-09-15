using System.Collections;
using System.Collections.Generic;
using Classes;
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
    private float _tweenTimer = 0.5f;
    private float _tweenTime = 0.5f;
    private bool _killAnimation = false;
    private bool _toBeDestroyed = false;
    public bool ToBeDestroyed => _toBeDestroyed;
    private bool interactable = true;
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

    public void Kill(bool instantly = false)
    {
        
        _toBeDestroyed = instantly;
        _killAnimation = true;
        interactable = false;
    } 

    // Update is called once per frame
    void Update()
    {
        if (_killAnimation)
        {
            var v = 1f - _tweenTimer / _tweenTime;
            var s = -1.25f * v * v + 0.25f * v + 1f;
            
            gameObject.transform.localScale= VectorTools.MutateVector3(gameObject.transform.localScale, 
                x: s*Screen.width / 1284f,
                y:s*Screen.width / 1284f);
            
            var r = gameObject.transform.rotation;
            gameObject.transform.rotation= Quaternion.Euler(
                r.eulerAngles.x,
                r.eulerAngles.y,
                    r.eulerAngles.z+200f*Time.deltaTime
                );
            
            /*
            gameObject.transform.rotation = Quaternion.Euler(
                gameObject.transform.rotation.eulerAngles.x,
                gameObject.transform.rotation.y*57.3f,
                gameObject.transform.rotation.z*57.3f+ Time.deltaTime*100);
                */
            
            
            _tweenTimer -= Time.deltaTime;
            if (_tweenTimer <= 0)
            {
                _toBeDestroyed = true;
            }
            //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale)
        }
        
    }

    public bool Collides(Vector2 point)
    {
        
        return interactable && hitbox.OverlapPoint(point);
    }

    public void MoveDownTime()
    {
        var y= gameObject.transform.position.y - Time.deltaTime * fallSpeed;
        Tools.MutatePosition(gameObject, y:y);
    }


}
