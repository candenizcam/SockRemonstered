using UnityEngine;

namespace Classes
{
    public class CameraTools
    {
        public readonly Camera Camera;
        public CameraTools(Camera c)
        {
            Camera = c;
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
        
    }
}