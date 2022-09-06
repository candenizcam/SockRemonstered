using UnityEngine;

namespace Classes
{
    public static class ColourTools
    {
        public static Color MutateColour(Color c, float? r = null, float? g= null, float? b = null, float? a= null)
        {
            return new Color(r ??= c.r, g ??= c.g, b ??= c.b, a ??= c.a);

        }
        
        public static Color Vector3ToColour(Vector3 v)
        {
            return new Color(v.x, v.y, v.z);
        }
        
        public static Vector3 ColourToVector3(Color c)
        {
            return new Vector3(c.r,c.g,c.b);
        }

        public static string ColourToHex(Color c)
        {
            var s = "#";
            float[] iter = {c.r, c.g, c.b};
            foreach (var VARIABLE in iter  )
            {
                s += ((int) (VARIABLE * 255)).ToString("x");
            }


            //$"{}"c.r*255
            return s;
        }
    }
}