using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Classes
{
    public class CameraTools
    {
        public readonly Camera Camera;
        
        //public static readonly float WorldHeight =  2532f / 200f;
        //public static readonly float UiHeight = 2532f;
        //public static readonly float UiWidth = 1170f;
        
        
        public float WorldWidth;
        
        // viewport
        protected float _unsafeLeft;
        protected float _unsafeRight;
        protected float _unsafeTop;
        protected float _unsafeBottom;
        protected float _safeHeight;
        protected float _safeWidth;



        
        
        public CameraTools(Camera c)
        {
            Camera = c;
            c.orthographicSize = Constants.WorldHeight;
            WorldWidth = c.aspect * c.orthographicSize;
            
            _unsafeLeft = Screen.safeArea.xMin/ Screen.width;
            _unsafeRight = (Screen.width -  Screen.safeArea.xMax)/ Screen.width;
            _unsafeBottom = Screen.safeArea.yMin/ Screen.height;
            _unsafeTop = (Screen.height - Screen.safeArea.yMax)/ Screen.height;

            
            _safeWidth = Screen.safeArea.width / Screen.width;
            _safeHeight = Screen.safeArea.height / Screen.height;
        }


        public float screen2wpWidth(float w)
        {
            var x2 = Camera.ScreenToWorldPoint(new Vector3(w, 0f, 0f)).x;
            var x1 = Camera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
            return x2-x1;
        }
        
        public float vp2wWidth(float w)
        {
            return Camera.ViewportToWorldPoint(new Vector3(w, 0f, 0f)).x;
        }
        
        public float vp2wHeight(float h)
        {
            return Camera.ViewportToWorldPoint(new Vector3(0f, h, 0f)).y;
        }
        
        public float vp2screenWidth(float w)
        {
            return Camera.ViewportToScreenPoint(new Vector3(w, 0f, 0f)).x;
        }
        
        public float vp2screenHeight(float h)
        {
            return Camera.ViewportToScreenPoint(new Vector3(0f, h, 0f)).y;
        }
        
        public Rect vp2wRect(Vector2 topRight, Vector2? bottomLeft = null)
        {
            var tr = Camera.ViewportToWorldPoint(topRight);
            var bl = Camera.ViewportToWorldPoint(bottomLeft ??= new Vector2(0f,0f));

            return Rect.MinMaxRect(bl.x, bl.y, tr.x, tr.y);


        }

        public Rect vp2wRect(float width, float height, Vector2? bottomLeft)
        {
            var bl = bottomLeft ??= new Vector2(0f, 0f);
            return vp2wRect(new Vector2(bl.x + width, bl.y + height), bl);

        }
        
        public Rect vp2wRect(float xMin, float yMin, float xMax, float yMax)
        {
            
            return vp2wRect(new Vector2(xMax,yMax),new Vector2(xMin,yMin));

        }
        
        public Rect vp2sRect(float width, float height, Vector2? bottomLeft)
        {
            var bl = bottomLeft ??= new Vector2(0f, 0f);
            return vp2sRect(new Vector2(bl.x + width, bl.y + height), bl);

        }
        
        public Rect vp2sRect(float xMin, float yMin, float xMax, float yMax)
        {
            
            return vp2sRect(new Vector2(xMax,yMax),new Vector2(xMin,yMin));

        }
        
        //public Rect vp2uiRect(float xMin, float yMin, float xMax, float yMax)
        //{
            
            //return vp2sRect(new Vector2(xMax,yMax),new Vector2(xMin,yMin));

        //}
        
        public Rect vp2sRect(Vector2 topRight, Vector2? bottomLeft = null)
        {
            var tr = Camera.ViewportToScreenPoint(topRight);
            var bl = Camera.ViewportToScreenPoint(bottomLeft ??= new Vector2(0f,0f));

            return Rect.MinMaxRect(bl.x, bl.y, tr.x, tr.y);


        }
        
        public enum  CoordSystem{Screen, World, Viewport, UI}
        
    }
}