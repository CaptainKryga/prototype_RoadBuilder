using System.Collections.Generic;
using Controller.CustomInput;
using Scriptable;
using UnityEngine;

namespace Model
{
    public class Evaluation : MonoBehaviour
    {
        [SerializeField] private CustomInputBase _customInput;
        [SerializeField] private DataGame _dataGame;
        
        private Queue<Cell>[] _paths;
        private Transform[] _cubes;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _customInput.InputKeyboard_Action += PrePush;
        }

        private void OnDisable()
        {
            _customInput.InputKeyboard_Action -= PrePush;
        }

        private void PrePush(KeyCode key, bool isDown)
        {
            if (key != KeyCode.Alpha6) return;
            
            _paths = new Queue<Cell>[4];
            _paths[0] = SetupStartVector(GameMetrics.PointA + Vector3Int.up);
            _paths[1] = SetupStartVector(GameMetrics.PointA - Vector3Int.up);
            _paths[2] = SetupStartVector(GameMetrics.PointA + Vector3Int.right);
            _paths[3] = SetupStartVector((GameMetrics.PointA - Vector3Int.right));

            _cubes = new Transform[4];
            _cubes[0] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            _cubes[1] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            _cubes[2] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            _cubes[3] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);

            PushEvaluation(_paths, _cubes);
        }

        private Queue<Cell> SetupStartVector(Vector3 startPos)
        {
            Queue<Cell> queue = new Queue<Cell>();
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(startPos, 0.11f);
            foreach (var coll in colliders)
            {
                Cell cell = coll.GetComponent<Cell>();
                if (cell)
                {
                    queue.Enqueue(cell);
                    Debug.Log("cell: " + cell.gameObject.name);
                    break;
                }
            }

            return queue;
        }

        private void PushEvaluation(Queue<Cell>[] paths, Transform[] cubes)
        {

        }
    }
}