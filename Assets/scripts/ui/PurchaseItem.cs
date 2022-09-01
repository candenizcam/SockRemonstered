using System;
using UnityEngine;

namespace Classes
{
    public class PurchaseItem: ButtonClickable
    {
        public PurchaseItem(float scale, Action clickAction): base(scale,"ui/shop/ShopListing",Color.gray,clickAction)
        {
            
        }
    }
}