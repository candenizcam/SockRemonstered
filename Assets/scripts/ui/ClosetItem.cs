using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class ClosetItem: ButtonClickable
    {
        public ClosetItem(float scale, Action clickAction): base(scale,"ui/shop/ClosetItem",Color.gray,clickAction)
        {
            
        }
    }
}