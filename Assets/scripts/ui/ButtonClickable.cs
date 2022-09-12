using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class ButtonClickable: VisualElement
    {
        public Action OnTouchDown = () => {};
        public Action OnTouchUp = () => {};
        public Action OnLeave = () => { };
        public Action ClickAction = () => {};

        
        
        public string Text
        {
            get => _textLabel.text;
            set => _textLabel.text = value;
        }
        
        
        protected float width;
        protected float height;

        private readonly Label _textLabel;
        public bool DisableButton = false;
        
        public ButtonClickable(Action clickAction= null) : base()
        {
            ClickAction = clickAction ??= () => {};
            style.borderBottomColor = Color.clear;
            style.borderTopColor = Color.clear;
            style.borderRightColor = Color.clear;
            style.borderLeftColor = Color.clear;
            _textLabel = new Label();
            _textLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            _textLabel.StretchToParentSize();
            Add(_textLabel);
            
            RegisterCallback<MouseLeaveEvent>(TouchLeave);
            RegisterCallback<MouseDownEvent>(TouchDown);
            RegisterCallback<MouseUpEvent>(TouchUp);
            RegisterCallback<ClickEvent>(Click);
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
            
            
            
        }

        
        
        public void Scale(float scale)
        {
            style.width = width * scale;
            style.height = height * scale;
        }

        
        
        protected virtual void TouchLeave(MouseLeaveEvent e)
        {
            if (!DisableButton)
            {
                OnLeave();
            }
        }
        
        protected virtual void TouchDown(MouseDownEvent e)
        {
            if (!DisableButton)
            {
                OnTouchDown();
            }
        }

        protected virtual void TouchUp(MouseUpEvent e)
        {
            if (!DisableButton)
            {
                OnTouchUp();
            }
        }

        protected virtual void Click(ClickEvent e)
        {
            if (!DisableButton)
            {
                ClickAction();
            }
        }

    }
}