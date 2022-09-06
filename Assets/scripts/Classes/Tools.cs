using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public static class Tools
{

    /** Generates a vector with given values, while filling the rest randomly between 0f-1f
     *  
     */
    public static Vector2 RandomVector2(float? x=null, float? y = null)
    {
        var a = new System.Random();
        //var a = new Unity.Mathematics.Random();
        
        float r1 = x ??= (float) a.NextDouble();
        float r2 = y ??= (float) a.NextDouble();
        return new Vector2(r1, r2);
    }

    
    
    
    
        
        public static void MutatePosition(GameObject g, float? x = null, float? y=null, float? z=null)
        {
            g.transform.position = VectorTools.MutateVector3(g.transform.position, x, y, z);
        }
        
        public static void MutatePosition(Transform g, float? x = null, float? y=null, float? z=null)
        {
            g.position = VectorTools.MutateVector3(g.position, x, y, z);
        }
        
        public static void TranslatePosition(GameObject g, float x = 0f, float y=0f, float z=0f)
        {
            var p = g.transform.position;
            g.transform.position = VectorTools.MutateVector3(g.transform.position, p.x+x, p.y+y, p.z+z);
        }
        
        public static void TranslatePosition(Transform g, float x = 0f, float y=0f, float z=0f)
        {
            var p = g.transform.position;
            g.position = VectorTools.MutateVector3(g.position, p.x+x, p.y+y, p.z+z);
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

        
        
        
        public static float[] CalcParabolaVertex(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            float A     = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
            float B     = (x3*x3 * (y1 - y2) + x2*x2 * (y3 - y1) + x1*x1 * (y2 - y3)) / denom;
            float C     = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

            float[] a = {A, B, C};
            return a;
        }

        /** returns the weight that fits in a width, height size with the given scale w/h
         * 
         */
        public static float WidthThatFitsToSize(float width, float height, float scale)
        {
            var otherH = width / scale;
            if (otherH > height)
            {
                return height * scale;
            }
            else
            {
                return width;
            }

        }

        public static List<int> IntRange(int to)
        {
            return IntRange(0, to);
        }
        
        public static List<int> IntRange(int from, int to)
        {
            var l = new List<int>();
            for (int i = from; i < to; i++)
            {
                l.Add(i);
            }
            return l;
        }
}
