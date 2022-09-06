using UnityEngine;

namespace Classes
{
    public class RectTools
    {
        public static Rect ScaleByCentre(Rect r, float widthScale, float heightScale)
        {
            var minX = r.center.x - r.width * 0.5f * widthScale;
            var maxX = r.center.x + r.width * 0.5f * widthScale;
            var minY = r.center.y - r.height * 0.5f * heightScale;
            var maxY = r.center.y + r.height * 0.5f * heightScale;

            return Rect.MinMaxRect(minX, minY, maxX, maxY);
        }
        
        public static Rect RectByCentre(float x, float y, float width, float height)
        {
            var minX = x - width * 0.5f;
            var maxX = x + width * 0.5f;
            var minY = y - height * 0.5f;
            var maxY = y + height * 0.5f;

            return Rect.MinMaxRect(minX, minY, maxX, maxY);
        }
    }
}