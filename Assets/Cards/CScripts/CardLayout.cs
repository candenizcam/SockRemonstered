using Classes;
using UnityEngine;

namespace Cards.CScripts
{
    public class CardLayout: CameraTools
    {

        private int _cols;
        private int _rows;
        
        public CardLayout(Camera c, int rows, int cols) : base(c)
        {
            _cols = cols;
            _rows = rows;
            
            
            
            
        }

        public void MoveCard(SockCardPrefabScript s, int r, int c )
        {
            
        }
        
    }
}