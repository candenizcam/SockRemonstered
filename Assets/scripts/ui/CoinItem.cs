using System;
using UnityEngine;

namespace Classes
{
    public class CoinItem: ButtonClickable
    {
        public CoinItem(float scale, Action clickAction): base(scale,"ui/shop/BackListing",Color.gray,clickAction)
        {
            
        }
    }
}