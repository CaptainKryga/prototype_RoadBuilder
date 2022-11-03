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

            RecursiveSetRoad(_map, PathFinder(_map, new Vector2Int(1, 1), GameMetrics.Points.PointB));
            PathFinderCleanUp(_map);
            
            RecursiveSetRoad(_map, PathFinder(_map, new Vector2Int(1, 1), GameMetrics.Points.PointB));
            PathFinderCleanUp(_map);
            
            RecursiveSetRoad(_map, PathFinder(_map, new Vector2Int(1, 1), GameMetrics.Points.PointB));
            PathFinderCleanUp(_map);
            
            RecursiveSetRoad(_map, PathFinder(_map, new Vector2Int(1, 1), GameMetrics.Points.PointB));
            PathFinderCleanUp(_map);
            
            VisibleMap(_map);
        }

        private void SetupPoints(byte[][] map)
        {
            map[1][1] = (byte)GameMetrics.Points.PointA;
            map[^2][^2] = (byte)GameMetrics.Points.PointB;
        }

        private Path PathFinder(byte[][] map, Vector2Int start, GameMetrics.Points end)
        {
            Queue<Path> queue = new Queue<Path>();
            byte index = (byte)GameMetrics.Points.PointB + 1;
            
            queue.Enqueue(new Path(start, null));
            Path path = null;

            while (queue.Count > 0)
            {
                path = queue.Dequeue();

                if (map[path.Pos.y][path.Pos.x] == (byte) end)
                    break;
                
                if (path.Pos.y + 1 < _sizeMap.y && map[path.Pos.y + 1][path.Pos.x] is 
                    (byte)GameMetrics.Points.Clear or (byte)GameMetrics.Points.PointB)
                {
                    PathFinderSetupCell(map, queue,path.Pos + Vector2Int.up, index, path);
                }
                if (path.Pos.y - 1 >= 0 && map[path.Pos.y - 1][path.Pos.x] is 
                    (byte)GameMetrics.Points.Clear or (byte)GameMetrics.Points.PointB)
                {
                    PathFinderSetupCell(map, queue,path.Pos - Vector2Int.up, index, path);
                }
                if (path.Pos.x + 1 < _sizeMap.x && map[path.Pos.y][path.Pos.x + 1] is 
                    (byte)GameMetrics.Points.Clear or (byte)GameMetrics.Points.PointB)
                {
                    PathFinderSetupCell(map, queue,path.Pos + Vector2Int.right, index, path);
                }
                if (path.Pos.x - 1 >= 0 && map[path.Pos.y][path.Pos.x - 1] is 
                    (byte)GameMetrics.Points.Clear or (byte)GameMetrics.Points.PointB)
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
            
            if (map[point.y][point.x] != (byte)GameMetrics.Points.PointB)
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

        private void RecursiveSetRoad(byte[][] map, Path parent)
        {
            if (parent.Child == null)
                return;

            if (map[parent.Child.Pos.y][parent.Child.Pos.x] > (byte)GameMetrics.Points.PointB)
            {
                if (parent.Pos.x == parent.Child.Child.Pos.x)
                {
                    //north-south
                    map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte)GameMetrics.Points.NorthSouth;
                }
                else if (parent.Pos.y == parent.Child.Child.Pos.y)
                {
                    map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte)GameMetrics.Points.WestEast;
                }
                else if (parent.Pos.x == parent.Child.Pos.x)
                {
                    if (parent.Pos.x > parent.Child.Child.Pos.x)
                        if (parent.Pos.y > parent.Child.Child.Pos.y)
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.WestNorth;
                        else
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.SouthWest;
                    else if (parent.Pos.x < parent.Child.Child.Pos.x)
                        if (parent.Pos.y > parent.Child.Child.Pos.y)
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.NorthEast;
                        else
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.EastSouth;
                    
                    // map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.Clear;
                }
                else if (parent.Pos.y == parent.Child.Pos.y)
                {
                    if (parent.Pos.y > parent.Child.Child.Pos.y)
                        if (parent.Pos.x > parent.Child.Child.Pos.x)
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.EastSouth;
                        else
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.SouthWest;
                    else if (parent.Pos.y < parent.Child.Child.Pos.y)
                        if (parent.Pos.x > parent.Child.Child.Pos.x)
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.NorthEast;
                        else
                            map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte) GameMetrics.Points.EastSouth;
                }
                else
                {
                    // map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte)GameMetrics.Points.EastSouth;
                    map[parent.Child.Pos.y][parent.Child.Pos.x] = (byte)GameMetrics.Points.Clear;
                }
            }
            
            RecursiveSetRoad(map, parent.Child);
            Debug.Log(parent.Child.Pos);
        }

        
        private void VisibleMap(byte[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map.Length; x++)
                {
                    if (map[y][x] >= (byte) GameMetrics.Points.Clear && 
                        map[y][x] <= (byte) GameMetrics.Points.PointB)
                    {
                        _cells[y][x] = Instantiate(_dataGame.GetPrefabFromType((GameMetrics.Points)map[y][x]),
                            new Vector3(x, y, 0), Quaternion.identity);
                        _cells[y][x].transform.Rotate(GameMetrics.RotatePoint((GameMetrics.Points)map[y][x]));
                        _cells[y][x].Debug.text = map[y][x].ToString();
                        _cells[y][x].Type = map[y][x];
                    }
                }
            }
        }
    }

    public class Path
    {
        public Vector2Int Pos;
        public Path Child;

        public Path(Vector2Int pos, Path child)
        {
            Pos = pos;
            Child = child;
        }
    }
}