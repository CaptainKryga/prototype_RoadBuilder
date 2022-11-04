using System.Collections;
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
        
        private Queue<Vector3>[] _paths;
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
            
            _paths = new Queue<Vector3>[1];
            _paths[0] = SetupStartVector(GameMetrics.PointA + Vector3Int.up);
            // _paths[1] = SetupStartVector(GameMetrics.PointA - Vector3Int.up);
            // _paths[2] = SetupStartVector(GameMetrics.PointA + Vector3Int.right);
            // _paths[3] = SetupStartVector((GameMetrics.PointA - Vector3Int.right));

            _cubes = new Transform[1];
            _cubes[0] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            // _cubes[1] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            // _cubes[2] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);
            // _cubes[3] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity);

            StartCoroutine(PushEvaluation(_paths, _cubes));
        }

        private Queue<Vector3> SetupStartVector(Vector3 startPos)
        {
            //PointA
            Queue<Vector3> queue = new Queue<Vector3>();
            queue.Enqueue(GameMetrics.PointA);
            
            //get next cell
            Collider2D[] results = new Collider2D[1];
            Physics2D.OverlapCircleNonAlloc(startPos, 0.1f, results);
            if (!results[0])
                return queue;
            
            Cell cell = results[0].GetComponent<Cell>();

            //get border PointA and Cell
            Vector3 nextPos = GameMetrics.PointA + (cell.Points[1].position - GameMetrics.PointA) / 2;
            queue.Enqueue(nextPos);

            int index = 0;
            while (cell)
            {
                if (cell.Type != (byte) GameMetrics.Points.PointB)
                {
                    if (cell.Points[0].position == nextPos)
                    {
                        queue.Enqueue(cell.Points[1].position);
                        queue.Enqueue(cell.Points[2].position);
                        nextPos = cell.Points[2].position;
                    }
                    else if (cell.Points[2].position == nextPos)
                    {
                        queue.Enqueue(cell.Points[1].position);
                        queue.Enqueue(cell.Points[0].position);
                        nextPos = cell.Points[0].position;
                    }
                }
                else
                {
                    queue.Enqueue(GameMetrics.PointB);
                    break;
                }

                results = new Collider2D[1];
                Physics2D.OverlapCircleNonAlloc(nextPos + (nextPos - cell.Points[1].position), 0.1f, results);

                if (!results[0])
                    break;
                cell = results[0].GetComponent<Cell>();
                queue.Enqueue(nextPos);

                if (index++ == 1000)
                    break;
            }

            return queue;
        }

        IEnumerator PushEvaluation(Queue<Vector3>[] paths, Transform[] cubes)
        {
            while (true)
            {
                bool flag = true;
                for (int x = 0; x < paths.Length; x++)
                {
                    if (paths[x].Count > 0)
                        flag = false;
                }
                if (flag)
                    break;
                
                for (int x = 0; x < paths.Length; x++)
                {
                    if (paths[x].Count == 0)
                        continue;

                    if (paths[x].Peek() != cubes[x].position)
                    {
                        cubes[x].position = Vector3.MoveTowards(cubes[x].position, paths[x].Peek(), Time.deltaTime);
                    }
                    else
                        paths[x].Dequeue();
                }
                
                yield return new WaitForEndOfFrame();
            }
            
            Debug.Log("COMPLETE");
            bool eval = true;
            for (int x = 0; x < cubes.Length; x++)
            {
                if (cubes[x].position != GameMetrics.PointB)
                    eval = false;
                
                Destroy(cubes[x].gameObject);
            }
            Debug.Log("Level " + (eval ? "WIN" : "DEFEAT"));
            yield break;
        }
    }
}