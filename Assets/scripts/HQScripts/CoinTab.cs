using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class CoinTab: ShopTab
    {
        
        public CoinTab(float scale, float width, float height): base(scale,width,height)
        {
            
        }
        
        
        public void UpdateShopItems(ShopItem[] shopItems)
        {
            _scrollView.Clear();
            _buttons.Clear();


            
            for (int i = 0; i < shopItems.Length; i++)
            {
                var thisItem = shopItems[i];
                var b = new CoinItem(_scale, () =>
                {
                    ItemFunction(thisItem);
                    
                });
                b.style.marginBottom = 43f * _scale;
                _buttons.Add(b);
                _scrollView.Add(b);
            }
         
        }

        
        
    }
}