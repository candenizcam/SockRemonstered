using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class CoinTab: VisualElement
    {
        private ScrollView _scrollView;
        private float _scale;
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public Action<ShopItem> coinAction;
        public CoinTab(float scale, float width, float height)
        {
            style.width = width;
            //style.height = height;
            _scale = scale;
            _scrollView= new ScrollView();
            Add(_scrollView);


            //_scrollView.style.height = style.height;
            _scrollView.style.width = style.width;

            _scrollView.mode = ScrollViewMode.Vertical;
            _scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
            _scrollView.scrollDecelerationRate = 0f;
            _scrollView.style.paddingLeft = 43f * scale;
            _scrollView.style.paddingRight = 43f * scale;
            _scrollView.style.paddingTop = 43f * scale;
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
                    CoinFunction(thisItem);
                    
                });
                b.style.paddingBottom = 43f * _scale;
                _buttons.Add(b);
                _scrollView.Add(b);
            }
         
        }

        void CoinFunction(ShopItem shopItem)
        {
            coinAction(shopItem);
        }
        
        
        public void Update()
        {
            foreach (var buttonClickable in _buttons)
            {
                buttonClickable.Update();
            }
        }
    }
}