using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class GenerateLevel : MonoBehaviour
    {
        [SerializeField] private Cell _prefab90, _prefab180, _prefabA, _prefabB;

        private byte[][] _map;

        private Vector2Int _sizeMap;
        public Vector2Int SizeMap
        {
            get => _sizeMap;
        }

        private enum Points
        {
            //0
            Clear,
            //90 degrees
            SouthNorth,
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

        private void Awake()
        {
            _sizeMap = new Vector2Int(10, 10);//Random.Range(4, 10);

            _map = new byte[_sizeMap.y][];
            for (int y = 0; y < _map.Length; y++)
                _map[y] = new byte[_sizeMap.x];
            
            SetupPoints(_map);
            
            PathFinder(_map);
            
            VisibleMap(_map);
        }

        private void VisibleMap(byte[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map.Length; x++)
                {
                    Cell cell = null;
                    if (map[y][x] == (byte) Points.PointA)
                    {
                        cell = Instantiate(_prefabA, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (map[y][x] == (byte) Points.PointB)
                    {
                        cell = Instantiate(_prefabB, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    else if (map[y][x] > (byte) Points.PointB)
                    {
                        cell = Instantiate(_prefab90, new Vector3(x, y, 0), Quaternion.identity);
                    }

                    if (cell)
                        cell.Debug.text = map[y][x].ToString();
                }   
            }
        }

        private void SetupPoints(byte[][] map)
        {
            map[0][0] = (byte)Points.PointA;
            map[^1][^1] = (byte)Points.PointB;
        }

        private void PathFinder(byte[][] map)
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            byte index = (byte)Points.PointB + 1;
            
            queue.Enqueue(new Vector2Int(0, 0));
            
            while (queue.Count > 0)
            {
                Vector2Int point = queue.Dequeue();
             
                if (map[point.y][point.x] == (byte)Points.PointB)
                    break;

                if (point.y + 1 < _sizeMap.y && map[point.y + 1][point.x] == (byte)Points.Clear)
                {
                    queue.Enqueue(point + Vector2Int.up);
                    map[point.y + 1][point.x] = index;
                }
                if (point.y - 1 >= 0 && map[point.y - 1][point.x] == (byte)Points.Clear)
                {
                    queue.Enqueue(point - Vector2Int.up);
                    map[point.y - 1][point.x] = index;
                }
                if (point.x + 1 < _sizeMap.x && map[point.y][point.x + 1] == (byte)Points.Clear)
                {
                    queue.Enqueue(point + Vector2Int.right);
                    map[point.y][point.x + 1] = index;
                }
                if (point.x - 1 >= 0 && map[point.y][point.x - 1] == (byte)Points.Clear)
                {
                    queue.Enqueue(point - Vector2Int.right);
                    map[point.y][point.x - 1] = index;
                }

                index++;
            }
        }
    }
}