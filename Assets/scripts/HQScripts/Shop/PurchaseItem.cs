using System;
using HQScripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class PurchaseItem: ButtonClickable
    {
        private ShopItem _thisItem;
        public PurchaseItem(float scale, ShopItem thisItem, Action clickAction): base(scale,"ui/shop/ShopListing",Color.gray,clickAction)
        {
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
            price.text =  StringTools.NumberToThreeDigits(thisItem.Price, start: " ");
            moneyZone.Add(price);
            
            
            Add(moneyZone);

        }
    }
}