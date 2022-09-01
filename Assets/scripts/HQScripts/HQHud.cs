using System;
using System.Collections.Generic;
using Classes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class HQHud
    {
        private VisualElement _topBar;
        private VisualElement _bottomBar;
        private Rect topBarRect;
        private Rect bottomBarRect;
        private float scale;
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        private LivesButton _livesButton;
        private CoinsButton _coinsButton;
        
        public Action SettingsButtonAction = () => {};
        public Action LivesButtonAction = () => {};
        public Action CoinsButtonAction = () => {};
        public Action OtherButtonAction = () => {};
        public Action AchiButtonAction = () => {};
        public Action ShopButtonAction = () => {};
        public Action PlayButtonAction = () => {};
        
        public HQHud(HQLayout hqLayout)
        {
            topBarRect = hqLayout.topBarRect();
            bottomBarRect = hqLayout.bottomBarRect();
            
            scale = hqLayout.Scale;
        
            
            _topBar = new VisualElement();

            _topBar.style.position = Position.Absolute;
            _topBar.style.left = topBarRect.x;
            _topBar.style.bottom = topBarRect.y;
            _topBar.style.height = topBarRect.height;
            _topBar.style.width = topBarRect.width;
            //_topBar.style.backgroundColor = Color.red;
            _topBar.style.flexDirection =FlexDirection.Row;
            _topBar.style.flexBasis = topBarRect.width;
            _topBar.style.alignItems= Align.Center;
            _topBar.style.justifyContent = Justify.SpaceAround;
            generateTopBarElements(_topBar);

            _bottomBar = new VisualElement();
        
            _bottomBar.style.position = Position.Absolute;
            _bottomBar.style.left = bottomBarRect.x;
            _bottomBar.style.bottom = bottomBarRect.y;
            _bottomBar.style.height = bottomBarRect.height;
            _bottomBar.style.width = bottomBarRect.width;
            
            _bottomBar.style.flexDirection =FlexDirection.Row;
            _bottomBar.style.flexBasis = topBarRect.width;
            _bottomBar.style.alignItems= Align.Center;
            _bottomBar.style.justifyContent = Justify.SpaceAround;
            generateBottomBarElements(_bottomBar);

            

        }

        public void UpdateInfo(int coins, int hearts, float remFloat)
        {
            _livesButton.UpdateHeartNo(hearts,remFloat);

            string[] l = {"","k","M","B","T","Q","S"};
            var c = coins;
            var s = "A LOT";
            for (var i = 0; i < l.Length; i++)
            {
                if (c < 1000)
                {
                    s = $"{c}{l[i]}";
                    break;
                }
                else
                {
                    c = c / 1000;
                }
            }
            _coinsButton.UpdateText(s);
        }

        void generateBottomBarElements(VisualElement bottomBar)
        {
            var achiButton = new ButtonClickable(scale,"ui/buttons/Achievements",Color.gray,AchiButtonFunction);
            
            var playButton = new ButtonClickable(scale,"ui/buttons/Play",Color.gray,PlayButtonFunction);
            
            var shopButton = new ButtonClickable(scale,"ui/buttons/Shop",Color.gray,ShopButtonFunction);
            
            
            bottomBar.Add(achiButton);
            bottomBar.Add(playButton);
            bottomBar.Add(shopButton);
            
            _buttons.Add(achiButton);
            _buttons.Add(playButton);
            _buttons.Add(shopButton);
        }

        void generateTopBarElements(VisualElement topBar)
        {
            var settingsButton = new ButtonClickable(scale,"ui/buttons/Settings",Color.gray,SettingsButtonFunction);
            
            _livesButton = new LivesButton(scale,LivesButtonFunction);
            
            _coinsButton = new CoinsButton(scale,CoinButtonFunction);
            
            var otherButton = new ButtonClickable(scale,"ui/buttons/Pause",Color.gray,OtherButtonFunction);
            
            topBar.Add(settingsButton);
            topBar.Add(_livesButton);
            topBar.Add(_coinsButton);
            topBar.Add(otherButton);
            
            _buttons.Add(settingsButton);
            _buttons.Add(_livesButton);
            _buttons.Add(_coinsButton);
            _buttons.Add(otherButton);
        }

        void AchiButtonFunction()
        {
            AchiButtonAction();
        }

        void PlayButtonFunction()
        {
            PlayButtonAction();
        }

        void ShopButtonFunction()
        {
            ShopButtonAction();
        }

        void SettingsButtonFunction()
        {
            SettingsButtonAction();
        }

        void LivesButtonFunction()
        {
            LivesButtonAction();
        }
        
        void CoinButtonFunction()
        {
            CoinsButtonAction();
        }
        
        void OtherButtonFunction()
        {
            OtherButtonAction();
        }
        
        
        
        
        public void Update()
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
    }
}