using UnityEngine;

namespace Model
{
    public class GenerateLevel : MonoBehaviour
    {
        [SerializeField] private Transform prefab;

        private Points[][] _map;

        private Vector2Int _sizeMap;
        public Vector2Int SizeMap
        {
            get => _sizeMap;
        }

        private enum Points
        {
            //0
            Clear,
            //Point A and B
            PointA,
            PointB,
            //90 degrees
            SouthNorth,
            WestEast,
            //180 degrees
            NorthEast,
            EastSouth,
            SouthWest,
            WestNorth,
            //Pathfinder
            Closed,
        }

        private void Awake()
        {
            _sizeMap = new Vector2Int(10, 10);//Random.Range(4, 10);

            _map = new Points[_sizeMap.y][];
            for (int y = 0; y < _map.Length; y++)
                _map[y] = new Points[_sizeMap.x];
            
            SetupPoints(_map);
            
            VisibleMap(_map);
        }

        private void VisibleMap(Points[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map.Length; x++)
                {
                    if (map[y][x] != Points.Clear)
                    {
                        Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }   
            }
        }

        private void SetupPoints(Points[][] map)
        {
            map[0][0] = Points.PointA;
            map[^1][^1] = Points.PointB;
        }

        private void PathFinder()
        {
            
        }
    }
}