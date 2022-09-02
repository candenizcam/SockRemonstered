using System;
using HQScripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class ClosetItem: ButtonClickable
    {
        private ShopItem _thisItem;
        public ClosetItem(float scale, ShopItem thisItem, Action clickAction): base(scale,"ui/shop/ClosetItem",Color.gray,clickAction)
        {
            _thisItem = thisItem;


            //style.alignContent = Align.Center;
            style.alignItems = Align.Center;
            
            
            var frame = new VisualElement();
            frame.style.position = Position.Absolute;
            
            frame.style.left = 36f * scale;
            frame.style.top = 36f * scale;
            frame.style.width = 392f * scale;
            frame.style.height = 320f * scale;
            //frame.style.backgroundColor = new Color(0f,0f,1f,0.3f);
            frame.style.justifyContent = Justify.Center;
            frame.style.alignContent = Align.Center;
            var frame2 = new Image();
            frame2.sprite =  Resources.Load<Sprite>(_thisItem.Location);
            frame.Add(frame2);
            Add(frame);

            var label = new Label();
            label.style.position = Position.Absolute;
            label.style.bottom = 24f * scale;
            label.style.fontSize = 64f * scale;
            label.text = thisItem.DisplayName;
            Add(label);

        }
    }
}