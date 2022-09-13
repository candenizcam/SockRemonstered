using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class PurchaseTab: ShopTab
    {


        public PurchaseTab(float scale, float width, float height) : base(scale, width, height)
        {
            
        }
        
        public void UpdateShopItems(ShopItem[] shopItems, int coins)
        {
            _scrollView.Clear();
            _buttons.Clear();

            
            Debug.Log(coins);
            for (int i = 0; i < shopItems.Length; i++)
            {
                var thisItem = shopItems[i];
                
                var b = new PurchaseItem(_scale,thisItem, () =>
                {
                    ItemFunction(thisItem);
                }, coins>= thisItem.Price ? Color.white: new Color(1f,1f,1f,0.3f));
                b.style.marginBottom = 43f * _scale;
                b.DisableButton = coins < thisItem.Price;
                
                _buttons.Add(b);
                _scrollView.Add(b);
            }
         
        }

    }
}