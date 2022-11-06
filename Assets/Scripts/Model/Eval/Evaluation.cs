using System.Collections;
using System.Collections.Generic;
using Controller.CustomInput;
using Model.Components;
using Model.Static;
using Scriptable;
using UnityEngine;
using View.Game;

namespace Model.Eval
{
    public class Evaluation : MonoBehaviour
    {
        [SerializeField] private CustomInputBase _customInput;
        [SerializeField] private DataGame _dataGame;
        [SerializeField] private ResultUI _resultUI;

        [SerializeField] private Transform _parent;
        
        private Queue<Vector3>[] _paths;
        private Transform[] _cubes;

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

            _paths = new Queue<Vector3>[GameMetrics.Paths.Length];
            _cubes = new Transform[GameMetrics.Paths.Length];
            for (int x = 0; x < GameMetrics.Paths.Length; x++)
            {
                _paths[x] = SetupStartVector(GameMetrics.PointA + GameMetrics.Paths[x]);
                _cubes[x] = Instantiate(_dataGame.PrefabCube, GameMetrics.PointA, Quaternion.identity, 
                    _parent);
            }

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
            
            Entity entity = results[0].GetComponent<Entity>();
            if (!entity || (entity && entity.IsStatic))
                return queue;
            
            Transform[] Points = ((CellDynamic) entity).Points;

            //get border PointA and Cell
            Vector3 nextPos = GameMetrics.PointA + (Points[1].position - GameMetrics.PointA) / 2;
            queue.Enqueue(nextPos);

            while (entity)
            {
                if (entity.IsStatic)
                {
                    queue.Enqueue(entity.Type == (byte) GameMetrics.Points.PointA ?
                        GameMetrics.PointA : GameMetrics.PointB);
                    break;
                }


                if (Points[0].position == nextPos)
                {
                    queue.Enqueue(Points[1].position);
                    queue.Enqueue(Points[2].position);
                    nextPos = Points[2].position;
                }
                else if (Points[2].position == nextPos)
                {
                    queue.Enqueue(Points[1].position);
                    queue.Enqueue(Points[0].position);
                    nextPos = Points[0].position;
                }
                else
                {
                    break;
                }

                results = new Collider2D[1];
                Physics2D.OverlapCircleNonAlloc(nextPos + (nextPos - Points[1].position), 0.1f, results);

                if (!results[0])
                    break;
                entity = results[0].GetComponent<Entity>();
                queue.Enqueue(nextPos);
                
                Points = !entity.IsStatic ? ((CellDynamic) entity).Points : null;
            }

            return queue;
        }

        public void Result()
        {
            PrePush(KeyCode.Alpha6, true);
        }

        private IEnumerator PushEvaluation(Queue<Vector3>[] paths, Transform[] cubes)
        {
            int score = 0;
            foreach (var path in paths)
            {
                score += path.Count;
            }
            
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
            _resultUI.GameOver(eval, score * (eval ? 10 : 1));
            yield break;
        }
    }
}