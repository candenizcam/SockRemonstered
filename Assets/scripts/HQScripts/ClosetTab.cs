using System;
using System.Collections.Generic;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class ClosetTab: VisualElement
    {

        private ScrollView _scrollView;
        private float _scale;
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public Action<ShopItem> closetAction;
        public ClosetTab(float scale, float width, float height)
        {
            style.backgroundColor = Color.magenta;
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

            /*
            for (int i = 0; i < 20; i++)
            {
                var a = new VisualElement();
                a.style.width = style.width;
                a.style.height = 100f;
                a.style.backgroundColor = new Color(0f,i/20f,0f);
                _scrollView.Add(a);
            }
            */

        }

        public void UpdateShopItems(ShopItem[] shopItems)
        {
            _scrollView.Clear();
            _buttons.Clear();


            var colCount = 2;
            var n = 0;
            var rowCount = shopItems.Length / 2;
            for (int i = 0; i <= rowCount; i++)
            {
                var a = new VisualElement();
                //a.style.width = style.width;
                a.style.flexDirection = FlexDirection.Row;
                a.style.justifyContent = Justify.SpaceBetween;
                a.style.alignContent = Align.Center;
                a.style.marginBottom = 43f * _scale;
                a.style.flexWrap = Wrap.Wrap;
                
                for (int j = 0; j < colCount; j++)
                {
                    if(n>shopItems.Length-1) break;
                    var thisItem = shopItems[n];
                    var b = new ClosetItem(_scale, () =>
                    {
                        ClosetFunction(thisItem);
                    });
                    a.Add(b);
                    _buttons.Add(b);
                    n += 1;

                }
                _scrollView.Add(a);


            }
         
        }

        void ClosetFunction(ShopItem thisItem)
        {
            closetAction(thisItem);
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
