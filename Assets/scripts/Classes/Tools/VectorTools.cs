using UnityEngine;

namespace Classes
{
    public static class VectorTools
    {
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
    }
}