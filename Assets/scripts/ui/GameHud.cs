using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class GameHud
    {
        
        protected VisualElement _topBar;
        protected VisualElement _bottomBar;

        
        //protected Rect topBarRect;
        //protected Rect bottomBarRect;
        protected float scale;
        
        
        public Action SettingsButtonAction = () => {};
        protected MonsterFaces _monsterFaces;
        protected MoveCounter _moveCounter;
        protected Label LevelNoLabel;
 
        public GameHud()
        {
            Initialize();
            
        }

        public void SetLevelNo(string s)
        {
            LevelNoLabel.text = s;
        }

        public virtual void Initialize(float topHeight = 220f, float bottomHeight = 200f)
        {
            scale = 1f;
            
            _topBar = new VisualElement();

            _topBar.style.position = Position.Absolute;
            _topBar.style.top = Constants.UnsafeTopUi;
            _topBar.style.bottom = 0f;
            _topBar.style.height = topHeight;
            _topBar.style.width = Constants.UiWidth;
            
            var settingsButton = new ButtonClickable(scale,"ui/buttons/Pause_new",Color.gray,() =>
            {
                settingsButtonFunction();
            });
            settingsButton.style.position = Position.Absolute;
            settingsButton.style.left = 32f*scale;
            settingsButton.style.bottom = 32f*scale;

            //var s2 = Resources.Load<Sprite>(imagePath);
            //width = s2.rect.width * scale;
            //height = s2.rect.height * scale;
            //style.width = width;
            //style.height = height;
            //style.backgroundImage = new StyleBackground(s2);

            var s = Resources.Load<Sprite>("ui/buttons/money_bg");
            
            LevelNoLabel = new Label
            {
                style =
                {
                    backgroundImage = new StyleBackground(s),
                    position = Position.Absolute,
                    bottom = 32f,
                    left = 64f + settingsButton.Width,
                    width = s.rect.width,
                    height = s.rect.height,
                    unityTextAlign = TextAnchor.MiddleCenter,
                    color = Constants.GameColours[11],
                    fontSize = s.rect.height*0.4f,
                    unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"))
                    
                },
                text = "Level 999"
            };


            _bottomBar = new VisualElement();
        
            _bottomBar.style.position = Position.Absolute;
            _bottomBar.style.left = 0f;
            _bottomBar.style.bottom = Constants.UnsafeBottomUi;
            _bottomBar.style.height = bottomHeight;
            _bottomBar.style.width = Constants.UiWidth;
            
            
            _bottomBar.Add(settingsButton);
            _bottomBar.Add(LevelNoLabel);
            
            var w = 382f;
            var h = 284f;
            //380 veya daha büyük

            var unsafeHeight = (Screen.height - Screen.safeArea.yMax);

            if (unsafeHeight + 284 > 380)
            {
                h = 284;
            }
            else
            {
                h = 380 - unsafeHeight;
            }
            
            _monsterFaces = new MonsterFaces(scale);
            _monsterFaces.Portrait.style.right = 306*scale- _monsterFaces.ScaledWidth;
            _monsterFaces.Portrait.style.top = (h-64f)*scale - _monsterFaces.ScaledHeight;
            _topBar.Add(_monsterFaces.Portrait);

            
            _moveCounter = new MoveCounter(scale);
            _moveCounter.MoveBg.style.right = (w -_moveCounter.MoveBg.sprite.rect.width)*scale;
            _moveCounter.MoveBg.style.top = (h-_moveCounter.MoveBg.sprite.rect.height)*scale;
            _topBar.Add(_moveCounter.MoveBg);


        }
        
        protected void settingsButtonFunction()
        {
            Debug.Log("old guy");
            SettingsButtonAction();
        }
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_topBar);
            ve.Add(_bottomBar);
        }
    
        public void RemoveFromVisualElement(VisualElement ve)
        {
            ve.Remove(_topBar);
            ve.Remove(_bottomBar);
        }
        
        public void setVisible(bool b)
        {
            _topBar.visible = b;
            _bottomBar.visible = b;

        }
        
        public void updateInfo([CanBeNull] string  moveLeft = null, MonsterMood? monsterMood = null)
        {
            if (moveLeft != null)
            {
                _moveCounter.UpdateMoves(moveLeft);
            }

            if (monsterMood != null)
            {
                _monsterFaces.ChangeMood((MonsterMood)monsterMood);
            }
        
        }
    }
}