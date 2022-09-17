using System;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class AboutUsPopup : VisualElement
    {
        private Button _bgButton;
        public Action CloseButtonAction = () => {};
        public Action ResetButtonAction = () => {};


        public AboutUsPopup()
        {
            _bgButton = new Button(CloseButtonFunction)
            {
                style =
                {
                    position = Position.Absolute,
                    left = -10f - Constants.UnsafeLeftUi,
                    top = -10f - Constants.UnsafeTopUi,
                    height = Constants.UiHeight+20f,
                    width = Constants.UiWidth+20f,
                    backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.5f),
                    borderBottomColor = Color.clear,
                    borderTopColor = Color.clear,
                    borderRightColor = Color.clear,
                    borderLeftColor = Color.clear
                }
            };
            Add(_bgButton);

            var thingSprite = new StyleBackground(Resources.Load<Sprite>("AboutUs/AboutUs"));
            
            var bgW = thingSprite.value.sprite.rect.width;
            var bgH = thingSprite.value.sprite.rect.height;
            
            var thing = new VisualElement
            {
                style =
                {
                    backgroundImage = thingSprite,
                    position = Position.Absolute,
                    left = (Constants.UiWidth - bgW) * 0.5f,
                    top = (Constants.UiHeight - bgH) * 0.5f,
                    width = bgW,
                    height = bgH,
                    unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"))
                }
            };

            var v = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    bottom = 52f,
                    flexDirection = FlexDirection.Column,
                    justifyContent = Justify.FlexEnd,
                    alignItems = Align.Center
                }
            };
            v.StretchToParentWidth();

            var b = new ButtonClickable(1f, "AboutUs/ResetGameSave", Color.gray, ResetButtonFunction);
            
            v.Add(b);
            
            thing.Add(v);
            
            
            
            var cross = new ButtonClickable(1f, "ui/x", Color.gray, CloseButtonFunction);
            cross.style.position = Position.Absolute;
            cross.style.top = 50f;
            cross.style.right = 50f;
            
            thing.Add(cross);
            Add(thing);
        }

        private void CloseButtonFunction()
        {
            CloseButtonAction();
        }

        private void ResetButtonFunction()
        {
            ResetButtonAction();
        }
        
        
        
    }
}