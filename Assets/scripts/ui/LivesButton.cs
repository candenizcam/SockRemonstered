using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class LivesButton: ButtonClickable
    {
        private int _maxHeart = 5;
        private List<Image> _hearts = new List<Image>();
        private float heartWidth;
        private int _heartCount;
        public LivesButton(float scale, Action clickAction) : base(scale,"ui/buttons/lives_bg",Color.gray,clickAction)
        {
            var s = Resources.Load<Sprite>("ui/buttons/life");
            heartWidth= s.rect.width * scale;
            for (int i = 0; i < _maxHeart; i++)
            {
                var h = new Image();
                h.sprite = s;
                h.style.width = heartWidth;
                h.style.height = s.rect.height * scale;
                h.style.backgroundColor = Color.clear;
                h.style.position = Position.Absolute;
                h.style.bottom = (height - s.rect.height*scale) / 2f;
                _hearts.Add(h);
            }
            
            
            onTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = Color.gray;
                foreach (var heart in _hearts)
                {
                    heart.tintColor = Color.gray;
                }
            };
            
            onTouchUp = () =>
            {
                UpdateHeartNo(_heartCount);
                style.unityBackgroundImageTintColor = Color.white;
            };
            
            //style.flexDirection =FlexDirection.Row;
            //style.flexBasis = style.width;
            //style.alignItems= Align.Center;
            //style.justifyContent = Justify.SpaceAround;
            
            
        }



        public void UpdateHeartNo(int h)
        {
            if (h > _maxHeart)
            {
                throw new Exception("too much heart");
            }
            _heartCount = h;

            Clear();
            if (h > 0)
            {
                

                var otherWidth = width - heartWidth * 0.5f;
                
                if ((heartWidth*h) > otherWidth)
                {
                    var q = (otherWidth -  h*heartWidth) / (h - 1);
                    for (int i = 0; i < h; i++)
                    {
                        _hearts[i].tintColor = Color.white;
                        _hearts[i].style.left = heartWidth*0.25f+q * (i) + heartWidth * i;
                        Add(_hearts[i]);
                    }
                }
                else
                {
                    var q = (width - h * heartWidth) / (h + 1);
                    for (int i = 0; i < h; i++)
                    {
                        _hearts[i].style.left = q * (i + 1) + heartWidth * i;
                        Add(_hearts[i]);
                    }
                }
                
            }
            else
            {
                _hearts[0].tintColor = Color.gray;
                _hearts[0].style.left = (width - heartWidth) / 2f ;
                Add(_hearts[0]);
            }
            
            
        }
        
    }
}