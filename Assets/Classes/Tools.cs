using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{

    public static Vector2 RandomVector2()
    {
        var a = new System.Random();
        //var a = new Unity.Mathematics.Random();
        float r1 = (float) a.NextDouble();
        float r2 = (float) a.NextDouble();
        return new Vector2(r1, r2);
    }

    
     public static Vector3 Vector3Scale(Vector3 v, float scalar)
        {
            return new Vector3(v.x *scalar, v.y *scalar, v.z *scalar);
        }

        public static Vector3 Vector3Add(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        
        public static Vector3 Vector3BiasedSum(Vector3 v1, Vector3 v2, float coeff)
        {
            var uncoeff = 1f - coeff;
            Debug.Log(coeff+uncoeff);
            return new Vector3(v1.x*uncoeff + v2.x*coeff, v1.y*uncoeff + v2.y*coeff, v1.z*uncoeff + v2.z*coeff);
        }
        
        public static Vector3 vector3Div(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
        }
        
        public static Vector3 vector3Mul(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
        
        public static Vector3 vector3Div(Vector2 v1, Vector2 v2)
        {
            return new Vector3(v1.x / v2.x, v1.y / v2.y,0);
        }
        
        public static Vector3 vector3Mul(Vector2 v1, Vector2 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, 0);
        }

        public static Vector3 MutateVector3(Vector3 v, float? x = null, float? y=null, float? z=null)
        {
            return new Vector3(x ??= v.x, y ??= v.y, z ??= v.z);
        }
        
        public static void MutatePosition(GameObject g, float? x = null, float? y=null, float? z=null)
        {
            g.transform.position = MutateVector3(g.transform.position, x, y, z);
        }

        public static Color MutateColour(Color c, float? r = null, float? g= null, float? b = null, float? a= null)
        {
            return new Color(r ??= c.r, g ??= c.g, b ??= c.b, a ??= c.a);

        }

        
        
        /** this function finds the distance between a line and a point,
         * line is given as a vector from origin to the other point
         * relative point is given as relative to the base of the line
         * this function is overloaded by a three point version, which is recommended for clarity
         */
        public static float lineDistanceToPoint(Vector2 line, Vector2 relativePoint)
        {
            
            float projectionLength = (relativePoint.x * line.x + relativePoint.y * line.y) / line.magnitude;
            return (projectionLength < 0)
                ? relativePoint.magnitude
                : (projectionLength >= line.magnitude
                    ? (relativePoint - line).magnitude
                    : (float) Math.Sqrt(Math.Pow(relativePoint.magnitude, 2) - Math.Pow(projectionLength, 2)));


        }


        /** finds distance between a line defined by the first two points, and the third point
         * 
         */
        public static float lineDistanceToPoint(Vector2 lineBase, Vector2 lineOther, Vector2 point)
        {
            return lineDistanceToPoint(lineOther - lineBase, point - lineBase);
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
