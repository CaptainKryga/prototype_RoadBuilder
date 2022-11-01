using System.Collections.Generic;
using Scriptable;
using UnityEngine;

namespace Model
{
    public class GenerateLevel : MonoBehaviour
    {
        [SerializeField] private DataGame _dataGame;

        private byte[][] _map;
        private Cell[][] _cells;

        private Vector2Int _sizeMap;
        public Vector2Int SizeMap
        {
            get => _sizeMap;
        }

        private void Awake()
        {
            _sizeMap = new Vector2Int(10, 10);//Random.Range(4, 10);

            _map = new byte[_sizeMap.y][];
            _cells = new Cell[_sizeMap.y][];
            for (int y = 0; y < _map.Length; y++)
            {
                _map[y] = new byte[_sizeMap.x];
                _cells[y] = new Cell[_sizeMap.x];
            }
            
            SetupPoints(_map);

            RecursiveSetRoad(_map, null, PathFinder(_map));
            PathFinderCleanUp(_map);
            
            VisibleMap(_map);
        }

        private void SetupPoints(byte[][] map)
        {
            map[0][0] = (byte)GameMetrics.Points.PointA;
            map[^1][^1] = (byte)GameMetrics.Points.PointB;
        }

        private Path PathFinder(byte[][] map)
        {
            Queue<Path> queue = new Queue<Path>();
            byte index = (byte)GameMetrics.Points.PointB + 1;
            
            queue.Enqueue(new Path(new Vector2Int(0, 0), null));
            Path path = null;

            while (queue.Count > 0)
            {
                path = queue.Dequeue();
 
                if (map[path.Pos.y][path.Pos.x] == (byte)GameMetrics.Points.PointB)
                    break;

                if (path.Pos.y + 1 < _sizeMap.y && map[path.Pos.y + 1][path.Pos.x] == (byte)GameMetrics.Points.Clear)
                {
                    PathFinderSetupCell(map, queue,path.Pos + Vector2Int.up, index, path);
                }
                if (path.Pos.y - 1 >= 0 && map[path.Pos.y - 1][path.Pos.x] == (byte)GameMetrics.Points.Clear)
                {
                    PathFinderSetupCell(map, queue,path.Pos - Vector2Int.up, index, path);
                }
                if (path.Pos.x + 1 < _sizeMap.x && map[path.Pos.y][path.Pos.x + 1] == (byte)GameMetrics.Points.Clear)
                {
                    PathFinderSetupCell(map, queue,path.Pos + Vector2Int.right, index, path);
                }
                if (path.Pos.x - 1 >= 0 && map[path.Pos.y][path.Pos.x - 1] == (byte)GameMetrics.Points.Clear)
                {
                    PathFinderSetupCell(map, queue,path.Pos - Vector2Int.right, index, path);
                }

                index++;
            }

            return path;
        }
        private void PathFinderSetupCell(byte[][] map, Queue<Path> queue, Vector2Int point, byte index, Path path)
        {
            queue.Enqueue(new Path(point, path));
            map[point.y][point.x] = index;
        }
        private void PathFinderCleanUp(byte[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map.Length; x++)
                {
                    if (map[y][x] > (byte) GameMetrics.Points.PointB)
                        map[y][x] = (byte) GameMetrics.Points.Clear;
                }
            }
        }
        
        
        private void RecursiveSetRoad(byte[][] map, Path parent, Path child)
        {
            if (child == null)
                return;

            if (map[child.Pos.y][child.Pos.x] > (byte)GameMetrics.Points.PointB)
            {
                Debug.Log(child.Pos + " | " + child.Pre.Pos);
                if (child.Pos == child.Pre.Pos - Vector2Int.up || child.Pos == child.Pre.Pos + Vector2Int.up)
                {
                    //90
                    if (parent != null && parent.Pos.x == child.Pos.x)
                        map[child.Pos.y][child.Pos.x] = (byte) GameMetrics.Points.NorthSouth;
                    //180
                    else
                        map[child.Pos.y][child.Pos.x] = (byte) GameMetrics.Points.NorthEast;
                }
                if (child.Pos == child.Pre.Pos - Vector2Int.right || child.Pos == child.Pre.Pos + Vector2Int.right)
                {
                    //90
                    if (parent != null && parent.Pos.y == child.Pos.y)
                        map[child.Pos.y][child.Pos.x] = (byte)GameMetrics.Points.WestEast;
                    //180
                    else
                        map[child.Pos.y][child.Pos.x] = (byte) GameMetrics.Points.NorthEast;
                }
            }
            
            RecursiveSetRoad(map, child, child.Pre);
        }

        
        private void VisibleMap(byte[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map.Length; x++)
                {
                    if (map[y][x] > (byte) GameMetrics.Points.Clear && 
                        map[y][x] <= (byte) GameMetrics.Points.PointB)
                    {
                        _cells[y][x] = Instantiate(_dataGame.GetPrefabFromType((GameMetrics.Points)map[y][x]),
                            new Vector3(x, y, 0), Quaternion.identity);
                        _cells[y][x].transform.Rotate(GameMetrics.RotatePoint((GameMetrics.Points)map[y][x]));
                        _cells[y][x].Debug.text = map[y][x].ToString();
                    }
                }
            }
        }
    }

    public class Path
    {
        public Vector2Int Pos;
        public Path Pre;

        public Path(Vector2Int pos, Path pre)
        {
            Pos = pos;
            Pre = pre;
        }
    }
}