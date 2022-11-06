using Controller.CustomInput;
using UnityEngine;

namespace Model
{
    public class ChangeSetPath : MonoBehaviour
    {
        [SerializeField] private CustomInputBase _customInput;
        private int _id;
        private Camera _camera;
        private PathArrow[] _arrows;
        private bool _isArrow = false;

        private int Id
        {
            set
            {
                if (value == 0 || value == 1)
                    _id = value;
            }
        }

        private void Start()
        {
            _camera = Camera.main;
            _arrows = FindObjectsOfType<PathArrow>();
        }
        
        public void SetPath(int id)
        {
            Id = id;
            
            for (int x = 0; x < _arrows.Length; x++)
            {
                _arrows[x].gameObject.SetActive(true);
            }

            _customInput.InputMouse_Action += MouseClick;
        }

        private void MouseClick(CustomInputBase.Mouse key, Vector2 mousePosition)
        {
            if (key == CustomInputBase.Mouse.Left)
            {
                Collider2D[] results = new Collider2D[1];
                Physics2D.OverlapCircleNonAlloc(_camera.ScreenToWorldPoint(mousePosition), 0.1f, results);
                if (!results[0])
                    return;
            
                PathArrow arrow = results[0].GetComponent<PathArrow>();

                GameMetrics.Paths[_id] = arrow.Vector;

                _customInput.InputMouse_Action -= MouseClick;
            }
        }
    }
}
