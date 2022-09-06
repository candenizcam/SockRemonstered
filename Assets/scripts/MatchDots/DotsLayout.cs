using System;
using Classes;
using Unity.Mathematics;
using UnityEngine;

namespace MatchDots
{
    public class DotsLayout: GameLayout
    {
        private Rect _fieldBox = Rect.MinMaxRect(-4.70f,-3.82f,4.70f,5.18f);
        private Rect _field = Rect.MinMaxRect(-470f,-382f,470f,518f);
        private Rect[,] _smallRects;
        public float ScaledSingleSize;
        public DotsLayout(Camera c, int rows, int cols, Rect? field = null) : base(c)
        {
            _fieldBox = field ??= _fieldBox;
            var sw = _fieldBox.width / cols;
            var sh = _fieldBox.height / rows;

            var singleSize = Math.Min(sw, sh);

            var width = singleSize * cols;
            var height = singleSize * rows;

            var fbc = _fieldBox.center;

            _field = Rect.MinMaxRect((fbc.x - width * 0.5f)*Scale, (fbc.y - height * 0.5f)*Scale, (fbc.x + width * 0.5f)*Scale, (fbc.y + height * 0.5f)*Scale);

            ScaledSingleSize = singleSize * Scale;

            _smallRects = new Rect[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                var yMax = _field.yMax - ScaledSingleSize * i;
                var yMin = _field.yMax - ScaledSingleSize * (i+1);
                for (int j = 0; j < cols; j++)
                {
                    var xMin = _field.xMin + ScaledSingleSize * j;
                    var xMax = _field.xMin + ScaledSingleSize * (j+1);
                    
                    
                    _smallRects[i,j]  = Rect.MinMaxRect(xMin,yMin,xMax,yMax);
                
                }
                
            }

        }

        public Rect GetGridRect(int r, int c)
        {
            return _smallRects[r-1, c-1];
        }

        public (int r, int c) WorldToGridPos(float x, float y)
        {
            if (!_field.Contains(new Vector2(x, y))) return (-1, -1);
            var c = (x - _field.xMin) / ScaledSingleSize + 1;
            var r = (_field.yMax - y) / ScaledSingleSize + 1;
            return ((int)r, (int)c);

        }
        
        public (int r, int c) WorldToGridPos(Vector2 q)
        {
            return WorldToGridPos(q.x, q.y);

        }
        
        public (int r, int c) ScreenToGridPos(Vector2 q)
        {
            return WorldToGridPos(Camera.ScreenToWorldPoint(q));
        }
    }
}