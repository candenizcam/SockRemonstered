using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace HQScripts
{
    public class ClosetTab: ShopTab
    {
        
        public ClosetTab(float scale, float width, float height): base(scale,width, height)
        {
            
        }

        public void UpdateShopItems(ShopItem[] shopItems)
        {
            _scrollView.Clear();
            _buttons.Clear();

            var sgd = SerialGameData.LoadOrGenerate();
            
            var colCount = 2;
            var n = 0;
            var rowCount = shopItems.Length / 2;
            for (int i = 0; i <= rowCount; i++)
            {
                var a = new VisualElement();
                //a.style.width = style.width;
                a.style.flexDirection = FlexDirection.Row;
                a.style.justifyContent = Justify.SpaceBetween;
                a.style.alignContent = Align.Center;
                a.style.marginBottom = 43f * _scale;
                a.style.paddingLeft = 23f * _scale;
                a.style.paddingRight = 23f * _scale;
                a.style.flexWrap = Wrap.Wrap;
                
                for (int j = 0; j < colCount; j++)
                {
                    if (n > shopItems.Length - 1)
                    {
                        a.style.justifyContent = Justify.FlexStart;
                        break;
                        
                    }
                    var thisItem = shopItems[n];

                    var pressed = thisItem.ShopItemType switch
                    {
                        ShopItemType.Cloth => sgd.lineup.Contains(thisItem.ID) ? 1 : 0,
                        ShopItemType.Furniture => sgd.activeFurnitures.Contains(thisItem.ID) ? 1 : 0,
                        _ => 0
                    };

                    var b = new ClosetItem(_scale,thisItem, (x) =>
                    {
                        ItemFunction(thisItem);
                    },pressed, true );
                    a.Add(b);
                    _buttons.Add(b);
                    n += 1;

                }
                _scrollView.Add(a);


            }
         
        }

        
        
    }
}
