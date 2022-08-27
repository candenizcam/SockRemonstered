using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SockCardPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> socks;
    public List<SpriteRenderer> cards;
    
    [NonSerialized]
    public int SelectedSockCard = 0;
    [NonSerialized]
    public bool ToBeDestroyed = false;

    public bool sockVisible = false;
    private int _selectedCardSprite = 0;
    private int _selectedSockSprite = 0;
    private Rect _hitboxRect;
    private float borderPixel =10f;    
    
    // Start is called before the first frame update
    
    public void ChangeCardSprite(int cs)
    {
        _selectedCardSprite = cs;
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(i == _selectedCardSprite);
        }
    }
    
    void Awake()
    {
        ChangeCardSprite(0);
        for (int i = 0; i < socks.Count; i++)
        {
            socks[i].gameObject.SetActive(false);
        }
    }

    public void Resize(Vector3 position, Vector3 scale)
    {
        gameObject.transform.position = position;
        _hitboxRect = Rect.MinMaxRect(
            position.x - scale.x * 0.5f, position.y - scale.y * 0.5f,
            position.x + scale.x * 0.5f, position.y + scale.y * 0.5f);
        gameObject.transform.localScale = Tools.Vector3Add(Tools.vector3Div(scale,new Vector3(cards[0].size.x, cards[0].size.y, 1f)),new Vector3(-borderPixel/100f,-borderPixel/100f,0f));
    }

    public void SockVisible(bool b)
    {
        socks[SelectedSockCard].gameObject.SetActive(b);
        sockVisible = b;
    }

    public bool Collides(Vector2 point)
    {
        
        //gameObject.transform.localScale
        
        return _hitboxRect.Contains(point);
    }

    public void DestroyThis()
    {
        ToBeDestroyed = true;
        Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
