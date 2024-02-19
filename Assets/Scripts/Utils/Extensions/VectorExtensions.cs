using UnityEngine;

namespace Utils.Extensions
{
    public static class VectorExtensions
    {
        /// <returns> Copy of Vector3 to Vector2</returns>
        public static Vector2 ToVector2(this Vector3 self) => self;

        public static Vector2 ToXZVector2(this Vector3 self) => new(self.x, self.z);

        /// <returns> Copy of Vector2 to Vector3 with z = 0</returns>
        public static Vector3 ToVector3(this Vector2 self) => self;

        public static Vector3 ToXZVector3(this Vector2 self) => new(self.x, 0, self.y);

        ///  <returns> ints vector</returns>
        public static Vector3Int ToVInt(this Vector3 vector)
        {
            return new Vector3Int
            {
                x = Mathf.FloorToInt(vector.x),
                y = Mathf.FloorToInt(vector.y),
                z = Mathf.FloorToInt(vector.z)
            };
        }

        /// <summary> Set X of vector </summary>
        public static void SetX(this Vector3 self, float x) => self.x = x;

        /// <summary> Set Y of vector </summary>
        public static void SetY(this Vector3 self, float y) => self.y = y;

        /// <summary> Set Z of vector </summary>
        public static void SetZ(this Vector3 self, float z) => self.z = z;

        /// <summary> Add X to vector </summary>
        public static void AddX(this Vector3 self, float x) => self.x += x;

        /// <summary> Add Y to vector </summary>
        public static void AddY(this Vector3 self, float y) => self.y += y;

        /// <summary> Add Z to vector </summary>
        public static void AddZ(this Vector3 self, float z) => self.z += z;

        public static Vector2 RotateVector2(this Vector2 v, float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var sin = Mathf.Sin(radians);
            var cos = Mathf.Cos(radians);

            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }
    }
}