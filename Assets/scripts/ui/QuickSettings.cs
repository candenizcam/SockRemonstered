using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

namespace Classes
{
    public class QuickSettings
    {
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        private VisualElement _qsElements;
        public bool Active;
        public Action SettingsButtonAction  = () => {};
        public Action<bool> SoundButtonAction = (bool on) =>
        {
            Debug.Log($"sound {on}");
        };
        public Action<bool> MusicButtonAction = (bool on) => {};
        public Action ReturnButtonAction = () => {};
        private float scale;

        private ButtonClickable[] _soundButtons;
        private ButtonClickable[] _musicButtons;
        private ButtonClickable _returnButton;
        
        
        public QuickSettings(GameLayout gameLayout, int sound, int music)
        {
            scale = gameLayout.Scale;
            _qsElements = new VisualElement();
            _qsElements.style.position = Position.Absolute;
            _qsElements.style.width = Screen.width;
            _qsElements.style.height = Screen.height;
            _qsElements.style.left = 0f;
            _qsElements.style.top = 0f;
            _qsElements.style.backgroundColor = new Color(0.102f, 0.024f, 0.071f,0.84f);
            
            
            var settingsButton = new ButtonClickable(scale,"ui/buttons/Pause",Color.gray,() =>
            {
                settingsButtonFunction();
            });
            settingsButton.style.position = Position.Absolute;
            settingsButton.style.left = 32f*scale;
            settingsButton.style.bottom = gameLayout.bottomBarRect().yMin + 32f*scale;
            
            

            _buttons.Add(settingsButton);
            _qsElements.Add(settingsButton);

            _soundButtons = new ButtonClickable[2];
            
            _soundButtons[0] = new ButtonClickable(scale,"ui/buttons/sound_on",Color.gray,() =>
            {
                soundButtonFunction(true);
                //settingsButtonFunction();
            });
            _soundButtons[0].style.position = Position.Absolute;
            _soundButtons[0].style.left = 32f*scale;
            _soundButtons[0].style.bottom = gameLayout.bottomBarRect().yMin+ 250f*scale;
            _buttons.Add(_soundButtons[0]);
            
            
            _soundButtons[1] = new ButtonClickable(scale,"ui/buttons/sound_off",Color.gray,() =>
            {
                soundButtonFunction(false);
                //settingsButtonFunction();
            });
            _soundButtons[1].style.position = Position.Absolute;
            _soundButtons[1].style.left = 32f*scale;
            _soundButtons[1].style.bottom = gameLayout.bottomBarRect().yMin + 250f*scale;
            _qsElements.Add(_soundButtons[sound>0 ? 0: 1]);
            //_soundButtons[1].visible = false;
            

            _buttons.Add(_soundButtons[1]);
            //_qsElements.Add(_soundButtons[1]);
            
            _musicButtons = new ButtonClickable[2];
            _musicButtons[0] = new ButtonClickable(scale,"ui/buttons/music_on",Color.gray,() =>
            {
                musicButtonFunction(true);
                //settingsButtonFunction();
            });
            _musicButtons[0].style.position = Position.Absolute;
            _musicButtons[0].style.left = 32f*scale;
            _musicButtons[0].style.bottom = gameLayout.bottomBarRect().yMin+ 482f*scale;
            
            

            _buttons.Add(_musicButtons[0]);
            
            //_musicButtons[0].
            //_musicButtons[0].visible = true;
            _musicButtons[1] = new ButtonClickable(scale,"ui/buttons/music_off",Color.gray,() =>
            {
                musicButtonFunction(false);
                //settingsButtonFunction();
            });
            _musicButtons[1].style.position = Position.Absolute;
            _musicButtons[1].style.left = 32f*scale;
            _musicButtons[1].style.bottom = gameLayout.bottomBarRect().yMin+482f*scale;
            _qsElements.Add(_musicButtons[music>0 ? 0: 1]);
            

            _buttons.Add(_musicButtons[1]);
            //_qsElements.Add(_musicButtons[1]);
            //_musicButtons[1].visible = false;
            
            
            _returnButton = new ButtonClickable(scale,"ui/buttons/leave_merged",Color.gray,() =>
            {
                returnButtonFunction();
                //settingsButtonFunction();
            });
            _returnButton.style.position = Position.Absolute;
            _returnButton.style.left = 32f*scale;
            _returnButton.style.bottom = gameLayout.bottomBarRect().yMin+714f*scale;
            
            

            _buttons.Add(_returnButton);
            _qsElements.Add(_returnButton);
            
            //_bottomBar.Add(settingsButton);
            
        }

        private void settingsButtonFunction()
        {
            SettingsButtonAction();
        }
        
        private void musicButtonFunction(bool on)
        {
            if (on)
            {
                _qsElements.Remove(_musicButtons[0]);
                _qsElements.Add(_musicButtons[1]);
            }
            else
            {
                _qsElements.Remove(_musicButtons[1]);
                _qsElements.Add(_musicButtons[0]);
            }
            
            MusicButtonAction(on);
        }
        
        private void soundButtonFunction(bool on)
        {
            if (on)
            {
                _qsElements.Remove(_soundButtons[0]);
                _qsElements.Add(_soundButtons[1]);
            }
            else
            {
                _qsElements.Remove(_soundButtons[1]);
                _qsElements.Add(_soundButtons[0]);
            }
            
            SoundButtonAction(on);
        }
        
        private void returnButtonFunction()
        {
            ReturnButtonAction();
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
