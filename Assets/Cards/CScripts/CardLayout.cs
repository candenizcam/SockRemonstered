using System;
using Classes;
using UnityEngine;

namespace Cards.CScripts
{
    public class CardLayout: GameLayout
    {

        private int _cols;
        private int _rows;
        public Vector3[,] Centres;
        public Vector3 SingleScale;
        
        public CardLayout(Camera c, int rows, int cols) : base(c)
        {
            _cols = cols;
            _rows = rows;
            
            var cardScale = 1f; // w/h
            Centres = new Vector3[_rows,_cols];

            var w = Tools.WidthThatFitsToSize(_playfieldRectWorld.width,_playfieldRectWorld.height, ((float) _cols) /( (float) _rows));
            var h = w / _cols * _rows;
            
            var singleWidth = w / cols;
            var singleHeight = h / _rows;
            Debug.Log($"prw: {_playfieldRectWorld.width}, prwh: {_playfieldRectWorld.height}, sw: {singleWidth}");
            SingleScale = new Vector3(singleWidth, singleHeight, 1f);
            
            for(int i=0;i<rows;i++)
            {
                var y = (i - (rows-1f) * 0.5f) * singleHeight;
                for(int j=0;j<cols;j++)
                {
                    
                    var x = (j - (cols-1f) * 0.5f) * singleWidth;
                    Centres[i, j] = new Vector3(_playfieldRectWorld.center.x+x,_playfieldRectWorld.center.y+ y, 1f);
                }
            }

        }

        public void MoveCard(SockCardPrefabScript s, int r, int c )
        {
            
            
        }
        
        
        
    }
}