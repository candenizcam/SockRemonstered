using System;
using Classes;
using UnityEngine;

namespace WashingMachine.WMScripts
{
    public class WMLayout: CameraTools
    {

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

        
        
        public WMLayout(Camera c) : base(c)
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