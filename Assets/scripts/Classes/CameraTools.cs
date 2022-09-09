using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Classes
{
    public class CameraTools
    {
        public readonly Camera Camera;
        public CameraTools(Camera c)
        {
            Camera = c;
            Debug.Log($"sh: {Screen.height}, sw: {Screen.width}");
            c.orthographicSize = 2552f / 200f;
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
        
        public Rect vp2sRect(Vector2 topRight, Vector2? bottomLeft = null)
        {
            var tr = Camera.ViewportToScreenPoint(topRight);
            var bl = Camera.ViewportToScreenPoint(bottomLeft ??= new Vector2(0f,0f));

            return Rect.MinMaxRect(bl.x, bl.y, tr.x, tr.y);


        }
        
        public enum  CoordSystem{Screen, World, Viewport}
        
    }
}