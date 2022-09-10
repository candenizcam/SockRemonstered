using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class ButtonClickable: Button
    {
        public Action onTouchDown = () => {};
        public Action onTouchUp = () => {};

        protected float width;
        protected float height;
        
        public ButtonClickable(Action clickAction) : base(clickAction)
        {
            //OnMouseDown();
        
            style.borderBottomColor = Color.clear;
            style.borderTopColor = Color.clear;
            style.borderRightColor = Color.clear;
            style.borderLeftColor = Color.clear;
            
        }

        public ButtonClickable(float scale, string imagePath, Color pressedTint, Action clickAction): this(clickAction)
        {
            var s2 = Resources.Load<Sprite>(imagePath);
            width = s2.rect.width * scale;
            height = s2.rect.height * scale;
            style.width = width;
            style.height = height;
            style.backgroundImage = new StyleBackground(s2);
            style.backgroundColor = Color.clear;
            
            onTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = pressedTint;
            };
            
            onTouchUp = () =>
            {
                style.unityBackgroundImageTintColor = Color.white;
            };
            
        }

        public void Scale(float scale)
        {
            style.width = width * scale;
            style.height = height * scale;
        }
        
        public void TouchDown(Vector2 p)
        {
            if (worldBound.Contains(p))
            {
                onTouchDown();
            }
            
        }

        public void TouchUp(Vector2 p)
        {
            onTouchUp();
            if (worldBound.Contains(p))
            {
                
            }
            
        }

        public void Update()
        {
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    var p = Input.touches[0].position;
                    p = new Vector2(p.x/Screen.width*Constants.UiWidth, (Screen.height - p.y)/Screen.height*Constants.UiHeight);
                    if (worldBound.Contains(p))
                    {
                        onTouchDown();
                    }
                }
                
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    onTouchUp();
                }
                
                
            }
        }
        
        /*
        override protected void OnMouseDown()
        {
            
        }
        
        */
        
    }
}