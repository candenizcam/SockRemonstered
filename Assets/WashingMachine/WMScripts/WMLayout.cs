using System;
using Classes;
using UnityEngine;

namespace WashingMachine.WMScripts
{
    public class WMLayout: CameraTools
    {

        private const float _playfieldXMax = 0.9f;
        private const float _playfieldXMin = 0.05f;

        private float _unsafeLeft;
        private float _unsafeRight;
        private float _unsafeTop;
        private float _unsafeBottom;

        private float _safeHeight;
        private float _safeWidth;

        private Rect _playfieldRectWorld;
        private Rect _playfieldRectScreen;
        private Rect _playfieldRectViewport;
        
        public float playfieldTop // from unsafe top
        {
            get
            {
                return _unsafeTop+_safeHeight*(_playfieldXMax);
            }
        }
        
        public float playfieldBottom // from unsafe top
        {
            get
            {
                return _unsafeTop+_safeHeight*(_playfieldXMin);
            }
        }

        
        public Rect topBarRect 
        {
            get
            {
                return vp2sRect(_unsafeLeft, playfieldTop, Screen.safeArea.xMax/ Screen.width, 1f-_unsafeTop);
            }
        }
        
        public Rect bottomBarRect
        {
            get
            {
                Debug.Log($"xMin {_unsafeLeft}, ymin {_unsafeBottom}, xMax {Screen.safeArea.xMax/ Screen.width}, yMax {playfieldBottom}");
                return vp2sRect(_unsafeLeft,_unsafeBottom, Screen.safeArea.xMax/ Screen.width,playfieldBottom ) ;
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
        }

        
        
        
        
        
        public Rect playfieldRect(CoordSystem cs = CoordSystem.World)
        {
            switch (cs)
            {
                case CoordSystem.Screen:
                    return _playfieldRectScreen;
                    break;
                case CoordSystem.World:
                    return _playfieldRectWorld;
                    break;
                case CoordSystem.Viewport:
                    return _playfieldRectViewport;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cs), cs, null);
            }
            
        }
        
        
    }
}