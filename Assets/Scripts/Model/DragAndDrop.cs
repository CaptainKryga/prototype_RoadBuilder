using Controller.CustomInput;
using Model.Components;
using Model.Static;
using UnityEngine;

namespace Model
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField] private CustomInputBase _customInput;
        private CellDynamic _cellDynamic;
        
        private float _xMax, _yMax;
        private Camera _camera;
        
        private void OnEnable()
        {
            _customInput.InputMouse_Action += Take;
            _customInput.InputMouse_Action += Drop;
            _customInput.InputMouse_Action += Rotate;
        }

        private void OnDisable()
        {
            _customInput.InputMouse_Action -= Take;
            _customInput.InputMouse_Action -= Drop;
            _customInput.InputMouse_Action -= Rotate;
        }

        private void Start()
        {
            _xMax = GameMetrics.SizeSquare * GameMetrics.SizeMap.x;
            _yMax = GameMetrics.SizeSquare * GameMetrics.SizeMap.y;
            _camera = Camera.main;
        }

        private void Take(CustomInputBase.Mouse type, Vector2 mousePosition)
        {
            if (type != CustomInputBase.Mouse.ClickDown) return;

            Collider2D[] results = new Collider2D[1];
            Physics2D.OverlapCircleNonAlloc(_camera.ScreenToWorldPoint(mousePosition), 0.1f, results);
            
            if (!results[0]) return;
            
            _cellDynamic = results[0].GetComponent<CellDynamic>();
            if (_cellDynamic)
            {
                _customInput.InputMousePosition_Action += Drag;
            }
            else
            {
                _cellDynamic = null;
            }
        }

        private void Drag(Vector2 mousePosition)
        {
            Vector3 newPos = _camera.ScreenToWorldPoint(mousePosition);
            newPos.x = (int)RoundFloat(Mathf.Clamp(newPos.x, 0f, _xMax));
            newPos.y = (int)RoundFloat(Mathf.Clamp(newPos.y, 0f, _yMax));
            newPos.z = 0;
            
            Collider2D[] results = new Collider2D[1];
            Physics2D.OverlapCircleNonAlloc(newPos, 0.1f, results);
            if (results[0]) return;
            
            _cellDynamic.transform.position = newPos;
        }

        private float RoundFloat(float x)
        {
            x = Mathf.Abs(x);
            float del = x % 1f;
            if (del > 0.5f)
                return x + 0.5f;
            return x;
        }

        private void Drop(CustomInputBase.Mouse type, Vector2 mousePosition)
        {
            if (type != CustomInputBase.Mouse.ClickUp) return;
            
            _cellDynamic = null;
            
            _customInput.InputMousePosition_Action -= Drag;
        }

        private void Rotate(CustomInputBase.Mouse type, Vector2 axis)
        {
            if (type != CustomInputBase.Mouse.Scroll) return;

            if (_cellDynamic) _cellDynamic.transform.Rotate(Vector3.forward * 90 * (axis.x > 0 ? 1 : -1));
        }
    }
}
