using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockCardPrefabScript : MonoBehaviour
{
    public List<SpriteRenderer> socks;
    public List<SpriteRenderer> cards;
    
    [NonSerialized]
    public int SelectedSockCard = 0;

    private int _selectedCardSprite = 0;
    private int _selectedSockSprite = 0;
    
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
