using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace WashingMachine.WMScripts
{
    public class WMQuickSettings
    {
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        private VisualElement _qsElements;
        public bool Active;
        public Action SettingsButtonAction;
        public Action SoundButtonAction;
        public Action MusicButtonAction;
        public Action ReturnButtonAction;
        private float scale;
        
        
        public WMQuickSettings(WMLayout wmLayout)
        {
            scale = wmLayout.Scale;
            _qsElements = new VisualElement();
            _qsElements.style.position = Position.Absolute;
            _qsElements.style.width = Screen.width;
            _qsElements.style.height = Screen.height;
            _qsElements.style.left = 0f;
            _qsElements.style.top = 0f;
            _qsElements.style.backgroundColor = new Color(0.102f, 0.024f, 0.071f,0.84f);
            
            var s2 = Resources.Load<Sprite>("ui/buttons/Pause");
            var settingsButton = new ButtonClickable(() =>
            {
                settingsButtonFunction();
            });
            settingsButton.style.position = Position.Absolute;
            settingsButton.style.left = 32f*scale;
            settingsButton.style.bottom = 32f*scale;
            settingsButton.style.width = s2.rect.width * scale;
            settingsButton.style.height = s2.rect.height * scale;
            settingsButton.style.backgroundImage = new StyleBackground(s2);
            settingsButton.style.backgroundColor = Color.clear;
            settingsButton.onTouchDown = () =>
            {
                settingsButton.style.unityBackgroundImageTintColor = Color.gray;
            };
            
            settingsButton.onTouchUp = () =>
            {
                settingsButton.style.unityBackgroundImageTintColor = Color.white;
            };

            _buttons.Add(settingsButton);
            _qsElements.Add(settingsButton);
            
            //_bottomBar.Add(settingsButton);
            
        }

        private void settingsButtonFunction()
        {
            SettingsButtonAction();
        }
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_qsElements);
        }

        public void setVisible(bool b)
        {
            _qsElements.visible = b;
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