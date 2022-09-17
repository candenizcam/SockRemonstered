using System;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class SurePopup: VisualElement
    {
        public Action YesAction  = () => { };
        public Action NoAction  = () => { };

        public SurePopup()
        {
            var bgButton = new VisualElement()
            {
                style =
                {
                    position = Position.Absolute,
                    left = -10f - Constants.UnsafeLeftUi,
                    top = -10f - Constants.UnsafeTopUi,
                    height = Constants.UiHeight+20f,
                    width = Constants.UiWidth+20f,
                    backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.8f),
                    borderBottomColor = Color.clear,
                    borderTopColor = Color.clear,
                    borderRightColor = Color.clear,
                    borderLeftColor = Color.clear,
                    justifyContent = Justify.Center,
                    alignItems = Align.Center
                    
                }
            };
            Add(bgButton);
            
            
            var r =  Resources.Load<Sprite>("AboutUs/AreYouSure");
            var text = new VisualElement
            {
                style =
                {
                    width = r.rect.width,
                    height = r.rect.height,
                    backgroundImage = new StyleBackground(r),
                    marginBottom = 32f
                }
            };

            var noButton = new ButtonClickable(1f,"AboutUs/KeepSave",Color.gray,NoFunction);
            noButton.style.marginBottom = 32f;

            var yesButton = new ButtonClickable(1f,"AboutUs/DeleteSave",Color.gray,YesFunction);

            
            bgButton.Add(text);
            bgButton.Add(noButton);

            bgButton.Add(yesButton);
        }


        private void YesFunction()
        {
            YesAction();
        }

        private void NoFunction()
        {
            NoAction();
        }
        
        
    }
}