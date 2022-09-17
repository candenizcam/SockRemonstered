using System;
using System.Linq;
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
        private TutorialFrame[] _tutorialFrames;
        private StyleBackground[] _tutorialSprite;
        private float _tutorialTimeCounter = 0f;
        private int _tutorialIndex = 0;
        private bool _tutorialOn = false;
        
        
        public QuickSettings( int sound, int music, bool tutorialOn, TutorialFrame[] tutorialFrames)
        {

            _tutorialFrames = tutorialFrames;
            _tutorialSprite = tutorialFrames.Select(x => new StyleBackground(Resources.Load<Sprite>(x.Path))).ToArray();

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
                     left = 264f,
                     bottom = bottom+ 32f
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
                     left = 496f,
                     bottom = bottom+32f
                 }
             };
            _qsElements.Add(musicButton);
            
            var returnButton = new ButtonClickable(1f,"ui/buttons/leave_merged",Color.gray,ReturnButtonFunction)
             {
                 style =
                 {
                     position = Position.Absolute,
                     left = 728f,
                     bottom = bottom+32f
                 }
             };

            _qsElements.Add(returnButton);

            _tutorialOn = tutorialOn;
            var tutorialButton = new MultiButtonClickable(1f, a =>
            {
                //MusicButtonFunction(a==0);
                _tutorialOn = a == 0;

            }, new[] {"ui/buttons/tutorial_on", "ui/buttons/tutorial_off"}, Color.gray,startIndex:tutorialOn ? 1: 0)
            {
                style =
                {
                    position = Position.Absolute,
                    left = 960f,
                    bottom = bottom+32f
                }
            };
            _qsElements.Add(tutorialButton);
            
            
            TutorialRoll(0f);
        }


        public void TutorialRoll(float dt)
        {
            if (_tutorialOn)
            {
                if (_tutorialFrames.Length > 0)
                {
                
                    _tutorialTimeCounter += dt;
                
                    if (_tutorialTimeCounter >= _tutorialFrames[_tutorialIndex].StayTime)
                    {
                        _tutorialTimeCounter -= _tutorialFrames[_tutorialIndex].StayTime;
                        //Debug.Log($"{_tutorialIndex}, {_tutorialFrames[_tutorialIndex].Path}, {_tutorialSprite[_tutorialIndex]}");
                
                
                
                        _tutorialIndex += 1;
                        if (_tutorialIndex >= _tutorialFrames.Length)
                        {
                            _tutorialIndex -= _tutorialFrames.Length;
                        }

                        _qsElements.style.backgroundImage = _tutorialSprite[_tutorialIndex];

                    }
                }
            }
            else
            {
                _qsElements.style.backgroundImage = null;
            }
            
            
            
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
