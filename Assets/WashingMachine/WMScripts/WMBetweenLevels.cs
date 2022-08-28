using System;
using System.Collections.Generic;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace WashingMachine.WMScripts
{
    public class WMBetweenLevels
    {
        private VisualElement _betweenElement;
        private float scale = Screen.width / 1170f;
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public bool Active;
        private Label _bigText;
        private Label _smallText;
        private ButtonClickable _bigButton;
        public WMBetweenLevels(WMLayout wmLayout)
        {
            
            _betweenElement = new VisualElement();
            _betweenElement.style.width = Screen.width;
            _betweenElement.style.height = Screen.height;
            var bg = new Image();
            bg.sprite = Resources.Load<Sprite>("ui/betweenbg");
            bg.style.position = Position.Absolute;
            
            bg.style.left = (Screen.width - bg.sprite.rect.width) * 0.5f;
            bg.style.top = (Screen.height - bg.sprite.rect.height) * 0.5f;
            bg.style.width = bg.sprite.rect.width*scale;
            bg.style.height = bg.sprite.rect.height*scale;
            bg.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));

            _bigText = new Label();
            _smallText = new Label();

            _smallText.style.bottom = 312f * scale;
            _smallText.style.position = Position.Absolute;
            _smallText.style.left = 0f;
            _smallText.style.width = bg.sprite.rect.width*scale;
            _smallText.style.fontSize = 64f * scale;
            _smallText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _smallText.text = "Level failed!";
            
            
            _bigText.style.bottom = 419f * scale;
            _bigText.style.position = Position.Absolute;
            _bigText.style.fontSize = 109f * scale;
            _bigText.style.left = 0f;
            _bigText.style.width = bg.sprite.rect.width*scale;
            _bigText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _bigText.text = "LEVEL 320";

                _bigButton = new ButtonClickable(() =>
            {
                
            });

            var s = Resources.Load<Sprite>("ui/button");

            _bigButton.style.backgroundImage = new StyleBackground(s);
            _bigButton.style.position = Position.Absolute;
            _bigButton.style.bottom = 63*scale;
            _bigButton.style.left = (bg.sprite.rect.width*scale - s.rect.width*scale) * 0.5f;
            _bigButton.style.width= s.rect.width*scale;
            _bigButton.style.height= s.rect.height*scale;
            _bigButton.style.backgroundColor = Color.clear;
            
            _bigButton.style.fontSize = 80f*scale;
            _bigButton.text = "CONTINUE";
            
            _buttons.Add(_bigButton);
            var l = new Label();
            _bigButton.onTouchDown = () =>
            {
                _bigButton.style.unityBackgroundImageTintColor = Color.gray;
            };
            
            _bigButton.onTouchUp = () =>
            {
                _bigButton.style.unityBackgroundImageTintColor = Color.white;
            };
            
            
            
            
            bg.Add(_bigButton);
            bg.Add(_smallText);
            bg.Add(_bigText);
            _betweenElement.Add(bg);
        }
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_betweenElement);
        }

        public void setVisible(bool b)
        {
            _betweenElement.visible = b;
            Active = b;
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