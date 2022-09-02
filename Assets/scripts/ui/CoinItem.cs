using System;
using HQScripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class CoinItem: ButtonClickable
    {
        private ShopItem _thisItem;
        public CoinItem(float scale, ShopItem thisItem, Action clickAction): base(scale,"ui/shop/BackListing",Color.gray,clickAction)
        {
            _thisItem = thisItem;
            //style.alignItems = Align.Center;
            style.alignContent = Align.Center;
            
            var frame = new VisualElement();
            frame.style.position = Position.Absolute;
            frame.style.left = 32f * scale;
            
            frame.style.top = 32f * scale;
            
            frame.style.width = 200f * scale;
            frame.style.height = 200f * scale;
            frame.style.justifyContent = Justify.Center;
            frame.style.alignContent = Align.Center;
            
            var frame2 = new Image();
            frame2.sprite =  Resources.Load<Sprite>(_thisItem.Location);
            frame.Add(frame2);
            Add(frame);
            
            var bigLabel = new Label();
            bigLabel.style.fontSize = 64f * scale;
            bigLabel.text = thisItem.DisplayName;
            bigLabel.style.position = Position.Absolute;
            bigLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            bigLabel.style.right = 470f * scale;
            bigLabel.style.top = 88f * scale;
            Add(bigLabel);
            
            
            
            
            
            var smallLabel = new Label();
            smallLabel.style.position = Position.Absolute;
            //smallLabel.style.top = 106f * scale;
            smallLabel.style.right = 60f * scale;
            smallLabel.style.top = 88f * scale;
            smallLabel.style.width = 340f * scale;
            smallLabel.style.fontSize = 64f * scale;
            smallLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            smallLabel.text = thisItem.SmallText;
            Add(smallLabel);
            
            
            
            /*
             _thisItem = thisItem;
            style.alignItems = Align.Center;
            
            var frame = new VisualElement();
            frame.style.position = Position.Absolute;
            frame.style.left = 0f * scale;
            frame.style.top = 2f * scale;
            frame.style.width = 392f * scale;
            frame.style.height = 320f * scale;
            frame.style.justifyContent = Justify.Center;
            frame.style.alignContent = Align.Center;
            var frame2 = new Image();
            frame2.sprite =  Resources.Load<Sprite>(_thisItem.Location);
            frame.Add(frame2);
            Add(frame);
            
            var bigLabel = new Label();
            bigLabel.style.position = Position.Absolute;
            bigLabel.style.top = 26f * scale;
            bigLabel.style.left = 420f * scale;
            bigLabel.style.fontSize = 64f * scale;
            bigLabel.text = thisItem.DisplayName;
            Add(bigLabel);
            
            var smallLabel = new Label();
            smallLabel.style.position = Position.Absolute;
            smallLabel.style.top = 106f * scale;
            smallLabel.style.left = 420f * scale;
            smallLabel.style.width = 550f * scale;
            smallLabel.style.fontSize = 36f * scale;
            smallLabel.style.unityTextAlign = TextAnchor.UpperLeft;
            smallLabel.style.whiteSpace = WhiteSpace.Normal;
            smallLabel.text = thisItem.SmallText;
            Add(smallLabel);


            var moneyZone = new VisualElement();
            moneyZone.style.position = Position.Absolute;
            moneyZone.style.bottom = 2f * scale;
            moneyZone.style.right = 32f * scale;
            moneyZone.style.width = 300f * scale;
            moneyZone.style.height = 90f * scale;
            moneyZone.style.flexDirection = FlexDirection.Row;
            moneyZone.style.justifyContent = Justify.Center;
            moneyZone.style.alignItems= Align.Center;
            
            var coinFrame = new Image();
            coinFrame.sprite =  Resources.Load<Sprite>("ui/buttons/coin");
            coinFrame.style.width = 50f * scale;
            coinFrame.style.height = 50f * scale;
            moneyZone.Add(coinFrame);
            
            var price = new Label();
            price.style.fontSize = 64f * scale;
            price.text =  StringTools.NumberToThreeDigits(thisItem.Price);
            moneyZone.Add(price);
            
            
            Add(moneyZone);
             */
        }
    }
}