using System;
using Classes;
using UnityEngine;

namespace Cards.CScripts
{
    public class CardLayout: CameraTools
    {

        private int _cols;
        private int _rows;
        public Vector3[,] Centres;
        public Vector3 SingleScale;
        
        private const float _playfieldXMax = 0.85f;
        private const float _playfieldXMin = 0.1f;

        private float _unsafeLeft;
        private float _unsafeRight;
        private float _unsafeTop;
        private float _unsafeBottom;

        private float _safeHeight;
        private float _safeWidth;

        private Rect _playfieldRectWorld;
        private Rect _playfieldRectScreen;
        private Rect _playfieldRectViewport;
        
        private Rect _topBarRectWorld;
        private Rect _topBarRectScreen;
        private Rect _topBarRectViewport;
        
        private Rect _bottomBarRectWorld;
        private Rect _bottomBarRectScreen;
        private Rect _bottomBarRectViewport;
        
        public  float Scale = Screen.width / 1170f;
        
        public float playfieldTop // from unsafe top
        {
            get
            {
                //Debug.Log($"unsafe top: {_unsafeTop}, other: {_safeHeight*(_playfieldXMax)}, {Screen.safeArea.height} ");
                
                //return _unsafeTop+_safeHeight*(_playfieldXMax);
                return _unsafeTop+_safeHeight-220f/Screen.safeArea.height;
            }
        }
        
        public float playfieldBottom // from unsafe top
        {
            get
            {
                return _unsafeTop+_safeHeight*(_playfieldXMin);
            }
        }

        
        public CardLayout(Camera c, int rows, int cols) : base(c)
        {
            
            
            
            _unsafeLeft = Screen.safeArea.xMin/ Screen.width;
            _unsafeRight = (Screen.width -  Screen.safeArea.xMax)/ Screen.width;
            _unsafeBottom = Screen.safeArea.yMin/ Screen.height;
            _unsafeTop = (Screen.height - Screen.safeArea.yMax)/ Screen.height;

            
            _safeWidth = Screen.safeArea.width / Screen.width;
            _safeHeight = Screen.safeArea.height / Screen.height;
            
            _playfieldRectWorld = vp2wRect(_unsafeLeft, playfieldBottom, Screen.safeArea.xMax/ Screen.width, playfieldTop);
            _playfieldRectScreen = vp2sRect(_unsafeLeft, playfieldBottom, Screen.safeArea.xMax/ Screen.width, playfieldTop);
            _playfieldRectViewport = Rect.MinMaxRect(_unsafeLeft, playfieldBottom, Screen.safeArea.xMax/ Screen.width, playfieldTop);
            
            _topBarRectWorld = vp2sRect(_unsafeLeft, playfieldTop, Screen.safeArea.xMax/ Screen.width, 1f-_unsafeTop);
            _topBarRectScreen = vp2wRect(_unsafeLeft, playfieldTop, Screen.safeArea.xMax/ Screen.width, 1f-_unsafeTop);
            _topBarRectViewport =  Rect.MinMaxRect(_unsafeLeft, playfieldTop, Screen.safeArea.xMax/ Screen.width, 1f-_unsafeTop);
        
            _bottomBarRectWorld = vp2sRect(_unsafeLeft,_unsafeBottom, Screen.safeArea.xMax/ Screen.width,playfieldBottom );
            _bottomBarRectScreen = vp2wRect(_unsafeLeft,_unsafeBottom, Screen.safeArea.xMax/ Screen.width,playfieldBottom );
            _bottomBarRectViewport = Rect.MinMaxRect(_unsafeLeft,_unsafeBottom, Screen.safeArea.xMax/ Screen.width,playfieldBottom );

            
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
        
        
        public Rect playfieldRect(CoordSystem cs = CoordSystem.World)
        {
            switch (cs)
            {
                case CoordSystem.Screen:
                    return _playfieldRectScreen;
                case CoordSystem.World:
                    return _playfieldRectWorld;
                case CoordSystem.Viewport:
                    return _playfieldRectViewport;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cs), cs, null);
            }
            
        }
        
        public Rect topBarRect(CoordSystem cs = CoordSystem.Screen)
        {
            switch (cs)
            {
                case CoordSystem.Screen:
                    return _topBarRectWorld;
                case CoordSystem.World:
                    return _topBarRectScreen;
                case CoordSystem.Viewport:
                    return _topBarRectViewport;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cs), cs, null);
            }
            
        }
        
        public Rect bottomBarRect(CoordSystem cs = CoordSystem.Screen)
        {
            switch (cs)
            {
                case CoordSystem.Screen:
                    return _bottomBarRectWorld;
                case CoordSystem.World:
                    return _bottomBarRectScreen;
                case CoordSystem.Viewport:
                    return _bottomBarRectViewport;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cs), cs, null);
            }
            
        }
        
    }
}