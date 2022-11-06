using Model;
using Model.Components;
using Model.Static;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "DataGame", menuName = "ScriptableObjects/DataGame", order = 1)]
    public class DataGame : ScriptableObject
    {
        [SerializeField] private Entity _prefabA;
        [SerializeField] private Entity _prefabB;
        [SerializeField] private Entity _prefab90;
        [SerializeField] private Entity _prefab180;

        [SerializeField] private Entity _prefabClear;

        public Transform PrefabCube;

        public Entity GetPrefabFromType(GameMetrics.Points type)
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