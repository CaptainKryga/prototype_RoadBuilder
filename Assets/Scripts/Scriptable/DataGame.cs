using Model;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "DataGame", menuName = "ScriptableObjects/DataGame", order = 1)]
    public class DataGame : ScriptableObject
    {
        [SerializeField] private Cell _prefabA;
        [SerializeField] private Cell _prefabB;
        [SerializeField] private Cell _prefab90;
        [SerializeField] private Cell _prefab180;

        [SerializeField] private Cell _prefabClear;

        public Cell GetPrefabFromType(GameMetrics.Points type)
        {
            switch (type)
            {
                case GameMetrics.Points.PointA: return _prefabA;
                case GameMetrics.Points.PointB: return _prefabB;
                
                case GameMetrics.Points.NorthSouth: return _prefab180;
                case GameMetrics.Points.WestEast: return _prefab180;
                
                case GameMetrics.Points.Clear: return _prefabClear;
                
                default: return _prefab90;
            }
        }
    }
}