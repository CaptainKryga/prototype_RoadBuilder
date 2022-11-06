
using Unity.VisualScripting;
using UnityEngine;

namespace Model
{
    public static class GameMetrics
    {
        public static Vector2Int SizeMap;
        public static float SizeSquare = 1;
        public static Vector3Int PointA;
        public static Vector3Int PointB;

        public static Vector3[] Paths;
        
        public enum Points
        {
            //0
            Clear,
            //90 degrees
            NorthSouth,
            WestEast,
            //180 degrees
            NorthEast,
            EastSouth,
            SouthWest,
            WestNorth,
            //Point's A and B
            PointA,
            PointB,
        }

        public static Vector3 RotatePoint(Points point)
        {
            switch (point)
            {
                case Points.NorthSouth: return Vector3.zero;
                case Points.WestEast: return Vector3.forward * 90;
                
                case Points.NorthEast: return Vector3.zero;
                case Points.WestNorth: return Vector3.forward * 90;
                case Points.SouthWest: return Vector3.forward * 180;
                case Points.EastSouth: return Vector3.forward * 270;
                default: return Vector3.zero;
            }
        }
    }
}
