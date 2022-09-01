﻿using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace Classes
{
    public class ShopTabs: VisualElement
    {

        private MultiButtonClickable _clothButton;
        private MultiButtonClickable _furniButton;
        private MultiButtonClickable _coinsButton;
        public Action<int> TabAction;
        
        public ShopTabs( float scale, Action<int> tabAction)
        {
            TabAction = tabAction;
            _clothButton = new MultiButtonClickable((x) =>
            {
                TabFunc(0);
            }, new string[] {"ui/shop/BankIcon","ui/shop/ClosetIcon" }, Color.gray, changeOnClick: false);
            _clothButton.Scale(scale);
            

            _furniButton = new MultiButtonClickable((x) =>
            {
                TabFunc(1);
            }, new string[] {"ui/shop/BankIcon","ui/shop/ClosetIcon" }, Color.gray, changeOnClick: false);
            _furniButton.Scale(scale);
            
            _coinsButton = new MultiButtonClickable((x) =>
            {
                TabFunc(2);
            }, new string[] {"ui/shop/BankIcon","ui/shop/ClosetIcon" }, Color.gray, changeOnClick: false);
            _coinsButton.Scale(scale);


            style.flexDirection = FlexDirection.Row;
            style.alignContent = Align.Center;
            style.justifyContent = Justify.SpaceBetween;
            style.marginBottom = 32f * scale;
            style.marginTop= 32f * scale;
            style.marginLeft = 32f * scale;
            style.marginRight = 32f * scale;
            
            Add(_clothButton);
            Add(_furniButton);
            Add(_coinsButton);
        }

        private void TabFunc(int i)
        {
            _clothButton.ChangeIndex(i==0 ? 1 : 0);
            _furniButton.ChangeIndex(i==1 ? 1 : 0);
            _coinsButton.ChangeIndex(i==2 ? 1 : 0);
            
            TabAction(i);
        }
        
        
        public void Update()
        {
            
            _clothButton.Update();
            _furniButton.Update();
            _coinsButton.Update();
        }
    }
}