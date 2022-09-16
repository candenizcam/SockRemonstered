using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace MatchDots
{
    public class DotsHud: GameHud
    {
        private VisualElement _targetHolder;
        protected VisualElement _fullScreen; // including unsafe
        private float _targetHolderWidth = 800f;
        public DotsHud() : base()
        {
            Initialize();
        }
        
        public override void Initialize(float topHeight = 220f, float bottomHeight = 200f)
        {
            base.Initialize(topHeight,bottomHeight);
            _targetHolder = new VisualElement()
            {
                style =
                {
                    position = Position.Absolute,
                    left = 25f,
                    width = _targetHolderWidth,
                    height = 120f,
                    bottom = 30f,
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceAround,
                    alignContent = Align.Center,
                    backgroundImage = new StyleBackground(Resources.Load<Sprite>("ui/hedefarkasi")) 
                }
            };
            
            
            _fullScreen = new VisualElement();
            _fullScreen.style.position = Position.Absolute;
            _fullScreen.style.top = -Constants.UnsafeTopUi;
            _fullScreen.style.bottom = -Constants.UiHeight;
            _fullScreen.style.left = -Constants.UnsafeLeftUi;
            _fullScreen.style.right = -Constants.UnsafeRightUi;
            _fullScreen.style.backgroundColor = new Color(0.1f,0.1f,0.1f,0.8f);
            _topBar.Add(_fullScreen);
            
            _topBar.Add(_targetHolder);
            
            
            
            
        }

        public void ClearBg()
        {
            _fullScreen.style.backgroundColor = Color.clear;
        }
        
        public void StartAnimation(float alpha)
        {
            _targetHolder.style.bottom = 80f * alpha - (Constants.UiHeight*0.5f - 220f + 180f) * (1f - alpha);
            _targetHolder.style.width = _targetHolderWidth*alpha + (Constants.UiWidth+100f)*(1f-alpha);
            _targetHolder.style.left = 25f * alpha - 50f*(1f-alpha);
            _targetHolder.style.height = 160f * alpha + 360f * (1f - alpha);
        }

        public void UpdateTargets(List<DotsTarget> dt)
        {
            _targetHolder.Clear();
            
            
            foreach (var dotsTarget in dt)
            {
                var frame = new VisualElement();
                frame.style.alignItems = Align.Center;
                frame.style.justifyContent = Justify.Center;
                frame.style.flexDirection = FlexDirection.Row;
                var number = new Label();
                number.style.fontSize = 64f * scale;
                number.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
                number.text = $" {dotsTarget.Amount}";
                number.style.color = Constants.GetDotColours(dotsTarget.Type);
                

                var image = new Image();
                image.sprite = Resources.Load<Sprite>(Constants.GetDotPath(dotsTarget.Type));
                image.style.height = 72f;
                image.style.width = 72f;
                frame.Add(image);
                
                frame.Add(number);
                
                _targetHolder.Add(frame);
            }
        }
        
    }
}