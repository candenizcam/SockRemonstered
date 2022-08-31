using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class LivesButton: ButtonClickable
    {
        private int _maxHeart = Constants.MaxHearts;
        private List<Image> _hearts = new List<Image>();
        private float heartWidth;
        private int _heartCount;
        private VisualElement _loader;
        private float _rem=1f;
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

            _loader = new VisualElement();
            _loader.style.position = Position.Absolute;
            _loader.style.right = 0f;
            _loader.style.bottom = 0f;
            _loader.style.height = height;
            _loader.style.width = 0f;
            _loader.style.backgroundColor = new Color(0.2f,0.2f,0.2f,0.2f);
            Add(_loader);
            
            onTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = Color.gray;
                foreach (var heart in _hearts)
                {
                    heart.tintColor = Color.gray;
                }

                _loader.visible = false;
            };
            
            onTouchUp = () =>
            {
                UpdateHeartNo(_heartCount,_rem);
                style.unityBackgroundImageTintColor = Color.white;
                _loader.visible = true;
            };
            
            //style.flexDirection =FlexDirection.Row;
            //style.flexBasis = style.width;
            //style.alignItems= Align.Center;
            //style.justifyContent = Justify.SpaceAround;
            
            
        }



        public void UpdateHeartNo(int h, float rem = 1f)
        {
            if (h > _maxHeart)
            {
                throw new Exception("too much heart");
            }
            
            _rem = rem;
            _loader.style.width = width*(1f - rem);
            
            
            _heartCount = h;

            Clear();
            Add(_loader);
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