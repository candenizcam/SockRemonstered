using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class CoinsButton: ButtonClickable
    {
        private Label text;
        public CoinsButton(float scale, Action clickAction) : base(scale,"ui/buttons/money_bg",Color.gray, clickAction)
        {
            var s = Resources.Load<Sprite>("ui/buttons/coin");

            var margin = (height - s.rect.height * scale) / 2f;
            
            var h = new Image();
            h.sprite = s;
            h.style.width = s.rect.width*scale;
            h.style.height = s.rect.height * scale;
            h.style.backgroundColor = Color.clear;
            h.style.position = Position.Absolute;
            h.style.bottom = margin;
            h.style.left =  margin*0.75f;
            Add(h);

            text = new Label();
            text.style.width = width - (margin*3f + s.rect.width*scale) ;
            text.style.height = s.rect.height * scale;
            text.style.backgroundColor = Color.clear;
            text.style.position = Position.Absolute;
            text.style.bottom = margin;
            text.style.left = margin*1.5f + s.rect.width*scale;
            text.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));
            text.style.fontSize = 64f * scale;
            text.style.unityTextAlign =TextAnchor.MiddleCenter;
            text.style.color = Constants.GameColours[11];
            Add(text);
            
            onTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = Color.gray;
                h.tintColor = Color.gray;
            };
            
            onTouchUp = () =>
            {
                h.tintColor = Color.white;
                style.unityBackgroundImageTintColor = Color.white;
            };
        }

        public void UpdateText(string s)
        {
            text.text = s;
        }
        
    }
}