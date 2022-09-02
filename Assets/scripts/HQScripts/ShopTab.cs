using System;
using System.Collections.Generic;
using Classes;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class ShopTab: VisualElement
    {
        protected ScrollView _scrollView;
        protected float _scale;
        protected List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public Action<ShopItem> ItemAction;
        public ShopTab(float scale, float width, float height)
        {
            style.width = width;
            
            _scale = scale;
            _scrollView= new ScrollView();
            Add(_scrollView);


            //_scrollView.style.height = style.height;
            _scrollView.style.width = style.width;

            _scrollView.mode = ScrollViewMode.Vertical;
            _scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
            _scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            _scrollView.scrollDecelerationRate = 0f;
            //_scrollView.style.paddingLeft = 43f * scale;
            //_scrollView.style.paddingRight = 43f * scale;
            _scrollView.style.paddingTop = 43f * scale;
        }
        
        
        protected void ItemFunction(ShopItem shopItem)
        {
            ItemAction(shopItem);
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