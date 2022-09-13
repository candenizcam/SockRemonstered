﻿using System;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace ui
{
    public class QuickSettings
    {
        private readonly VisualElement _qsElements;
        public bool Active;
        public Action SettingsButtonAction  = () => {};
        public Action<bool> SoundButtonAction = (on) =>
        {
            Debug.Log($"sound {on}");
        };
        public Action<bool> MusicButtonAction = (on) => {Debug.Log($"music {on}");};
        public Action ReturnButtonAction = () => {};
        //private List<string> _tutorialPath = new List<string>();
        
        public QuickSettings( int sound, int music)
        {
            
            
            
            //_tutorialPath = tutorialPath;
            var bottom = Constants.UnsafeBottomUi;
            _qsElements = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    width = Constants.UiWidth,
                    height = Constants.UiHeight,
                    left = 0f,
                    top = 0f,
                    backgroundColor = new Color(0.102f, 0.024f, 0.071f,0.84f)
                }
            };

            var settingsButton = new ButtonClickable(1f,"ui/buttons/Pause-2",Color.gray,SettingsButtonFunction)
             {
                 style =
                 {
                     position = Position.Absolute,
                     left = 32f,
                     bottom = bottom + 32f
                 }
             };
            _qsElements.Add(settingsButton);

            var soundButton = new MultiButtonClickable(1f, a =>
            {
                SoundButtonFunction(a==0);

            }, new[] {"ui/buttons/sound_on", "ui/buttons/sound_off"}, Color.gray,startIndex:sound>0 ? 0: 1)
             {
                 style =
                 {
                     position = Position.Absolute,
                     left = 32f,
                     bottom = bottom+ 250f
                 }
             };
            _qsElements.Add(soundButton);
            
            var musicButton = new MultiButtonClickable(1f, a =>
            {
                MusicButtonFunction(a==0);

            }, new[] {"ui/buttons/music_on", "ui/buttons/music_off"}, Color.gray,startIndex:music>0 ? 0: 1)
             {
                 style =
                 {
                     position = Position.Absolute,
                     left = 32f,
                     bottom = bottom+482f
                 }
             };
            _qsElements.Add(musicButton);
            
            var returnButton = new ButtonClickable(1f,"ui/buttons/leave_merged",Color.gray,ReturnButtonFunction)
             {
                 style =
                 {
                     position = Position.Absolute,
                     left = 32f,
                     bottom = bottom+714f
                 }
             };

            _qsElements.Add(returnButton);
        }

        private void SettingsButtonFunction()
        {
            SettingsButtonAction();
        }
        
        private void MusicButtonFunction(bool on)
        {
            
            MusicButtonAction(on);
        }
        
        private void SoundButtonFunction(bool on)
        {
            SoundButtonAction(on);
        }
        
        private void ReturnButtonFunction()
        {
            ReturnButtonAction();
        }
        
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_qsElements);
        }
        
        public void RemoveFromVisualElement(VisualElement ve)
        {
            ve.Remove(_qsElements);
        }

        public void SetVisible(bool b)
        {
            _qsElements.visible = b;
            
            Active = b;
        }
        
    }
}
