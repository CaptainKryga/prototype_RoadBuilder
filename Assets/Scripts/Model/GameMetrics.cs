
using UnityEngine;

namespace Model
{
    public static class GameMetrics
    {
        public static float SizeSquare = 1;
        
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
            //Point A and B
            PointA,
            PointB,
        }

        public static Vector3 RotatePoint(Points point)
        {
            switch (point)
            {
                case Points.EastSouth: return Vector3.forward * 90;
                case Points.NorthEast: return Vector3.zero;
                default: return Vector3.zero;
            }
        }
    }
}
