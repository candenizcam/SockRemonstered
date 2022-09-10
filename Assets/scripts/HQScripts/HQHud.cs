﻿using System;
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
            _topBar = new VisualElement();

            _topBar.style.position = Position.Absolute;
            _topBar.style.left = 0f;
            _topBar.style.top = 0f;
            _topBar.style.height = 190f;
            _topBar.style.width = Constants.UiWidth;
            _topBar.style.flexDirection =FlexDirection.Row;
            _topBar.style.flexBasis = Constants.UiWidth;
            _topBar.style.alignItems= Align.Center;
            _topBar.style.justifyContent = Justify.SpaceAround;

            var topVisual = new Image();
            topVisual.sprite  = Resources.Load<Sprite>("ui/top");
            topVisual.style.position = Position.Absolute;
            topVisual.style.bottom = -60f;
            topVisual.style.left = 0f;
            
            topVisual.style.width = Constants.UiWidth;
            _topBar.Add(topVisual);
            
            generateTopBarElements(_topBar);

            _bottomBar = new VisualElement();
            _bottomBar.style.position = Position.Absolute;
            _bottomBar.style.left = 0f;
            _bottomBar.style.bottom = 0f;
            _bottomBar.style.height = 200f;
            _bottomBar.style.width = Constants.UiWidth;
            _bottomBar.style.flexDirection =FlexDirection.Row;
            _bottomBar.style.alignItems= Align.Center;
            _bottomBar.style.justifyContent = Justify.SpaceAround;
            
            var bottomVisual = new Image();
            bottomVisual.sprite  = Resources.Load<Sprite>("ui/bottom");
            bottomVisual.style.position = Position.Absolute;
            bottomVisual.style.top = -50f;
            bottomVisual.style.left = 0f;
            bottomVisual.style.width = Constants.UiWidth;
            _bottomBar.Add(bottomVisual);
            
            generateBottomBarElements(_bottomBar);
        }

        public void UpdateInfo(int coins, int hearts, float remFloat)
        {
            _livesButton.UpdateHeartNo(hearts,remFloat);
            _coinsButton.UpdateText(StringTools.NumberToThreeDigits(coins));
        }

        void generateBottomBarElements(VisualElement bottomBar)
        {
            var achiButton = new ButtonClickable(1f,"ui/buttons/Achievements",Color.gray,AchiButtonFunction);
            
            var playButton = new ButtonClickable(1f,"ui/buttons/Play",Color.gray,PlayButtonFunction);
            
            var shopButton = new ButtonClickable(1f,"ui/buttons/Shop",Color.gray,ShopButtonFunction);
            
            
            bottomBar.Add(achiButton);
            bottomBar.Add(playButton);
            bottomBar.Add(shopButton);
            
            _buttons.Add(achiButton);
            _buttons.Add(playButton);
            _buttons.Add(shopButton);
        }

        void generateTopBarElements(VisualElement topBar)
        {
            var settingsButton = new ButtonClickable(1f,"ui/buttons/Settings",Color.gray,SettingsButtonFunction);
            
            _livesButton = new LivesButton(1f,LivesButtonFunction);
            
            _coinsButton = new CoinsButton(1f,CoinButtonFunction);
            
            var otherButton = new ButtonClickable(1f,"ui/buttons/Pause",Color.gray,OtherButtonFunction);
            
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