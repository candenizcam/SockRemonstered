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
        protected Rect topBarRect;
        protected Rect bottomBarRect;
        protected float scale;
        protected List<ButtonClickable> _buttons = new List<ButtonClickable>();
        
        public Action SettingsButtonAction = () => {};
        protected MonsterFaces _monsterFaces;
        protected MoveCounter _moveCounter;
        
        public GameHud()
        {
        }
        
        public GameHud(GameLayout gl)
        {
            Initialize(gl);
            
        }

        public virtual void Initialize(GameLayout gl)
        {
            topBarRect = gl.topBarRect();
            bottomBarRect = gl.bottomBarRect();
            
            scale = gl.Scale;
        
            
            _topBar = new VisualElement();

            _topBar.style.position = Position.Absolute;
            _topBar.style.left = topBarRect.x;
            _topBar.style.bottom = topBarRect.y;
            _topBar.style.height = topBarRect.height;
            _topBar.style.width = topBarRect.width;
            
            var settingsButton = new ButtonClickable(scale,"ui/buttons/Pause",Color.gray,() =>
            {
                settingsButtonFunction();
            });
            settingsButton.style.position = Position.Absolute;
            settingsButton.style.left = 32f*scale;
            settingsButton.style.bottom = 32f*scale;
        

            _bottomBar = new VisualElement();
        
            _bottomBar.style.position = Position.Absolute;
            _bottomBar.style.left = bottomBarRect.x;
            _bottomBar.style.bottom = bottomBarRect.y;
            _bottomBar.style.height = bottomBarRect.height;
            _bottomBar.style.width = bottomBarRect.width;
            
            _buttons.Add(settingsButton);
            _bottomBar.Add(settingsButton);
            
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
        
        public virtual void Update()
        {
            foreach (var buttonClickable in _buttons)
            {
                buttonClickable.Update();
            }
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