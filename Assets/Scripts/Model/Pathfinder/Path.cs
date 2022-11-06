using UnityEngine;

namespace Model.Pathfinder
{
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