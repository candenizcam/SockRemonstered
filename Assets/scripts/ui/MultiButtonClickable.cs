using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class MultiButtonClickable: ButtonClickable
    {

        private Action<int> _clickAction;
        private int _index;
        private List<StyleBackground> _sprites = new List<StyleBackground>();
        public bool ChangeOnClick = true;
        public MultiButtonClickable(float scale, Action<int> clickAction, string[] imagePaths, Color pressedTint,int startIndex=0, bool changeOnClick=true) : base()
        {
            ChangeOnClick = changeOnClick;
            _clickAction = clickAction;
            _index = startIndex;

            foreach (var imagePath in imagePaths)
            {
                _sprites.Add( new StyleBackground(Resources.Load<Sprite>(imagePath)));
            }

            width = _sprites[0].value.sprite.rect.width*scale;
            height = _sprites[0].value.sprite.rect.height*scale;
            style.backgroundImage = _sprites[_index];

            
            style.width = width;
            style.height = height;
            style.backgroundColor = Color.clear;
            
            OnTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = pressedTint;
            };
            
            OnTouchUp = () =>
            {
                style.unityBackgroundImageTintColor = Color.white;
            };
            OnLeave = () =>
            {
                style.unityBackgroundImageTintColor = Color.white;
            };
            
            //_clickAction();
        }

        protected override void Click(ClickEvent e)
        {
            _clickAction(_index);
            if (!ChangeOnClick) return;
            _index += 1;
            _index %= _sprites.Count;
            style.backgroundImage = _sprites[_index];
        }
        

        public void ChangeIndex(int i)
        {
            _index = i;
            style.backgroundImage = _sprites[_index];
        }
        
    }
}