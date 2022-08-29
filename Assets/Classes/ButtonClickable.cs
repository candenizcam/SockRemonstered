using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class ButtonClickable: Button
    {
        public Action onTouchDown = () => {};
        public Action onTouchUp = () => {};
        
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
            style.width = s2.rect.width * scale;
            style.height = s2.rect.height * scale;
            style.backgroundImage = new StyleBackground(s2);
            style.backgroundColor = Color.clear;
            
            onTouchDown = () =>
            {
                style.unityBackgroundImageTintColor = Color.gray;
            };
            
            onTouchUp = () =>
            {
                style.unityBackgroundImageTintColor = Color.white;
            };
            
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
                    p = new Vector2(p.x, Screen.height - p.y);
                    
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