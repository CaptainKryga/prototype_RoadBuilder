using System.Linq;
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
        private bool _isArrow;

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

            UpdateColorPaths();
        }
        
        public void SetPath(int id)
        {
            if (id == -1)
            {
                DisableArrows();
                return;
            }
                
            if (_isArrow)
                return;
            
            Id = id;
            
            for (int x = 0; x < _arrows.Length; x++)
            {
                _arrows[x].gameObject.SetActive(true);
            }

            _customInput.InputMouse_Action += MouseClick;
            _isArrow = true;
        }

        private void MouseClick(CustomInputBase.Mouse key, Vector2 mousePosition)
        {
            if (key == CustomInputBase.Mouse.ClickDown)
            {
                Collider2D[] results = new Collider2D[10];
                PathArrow arrow = null;
                Physics2D.OverlapCircleNonAlloc(_camera.ScreenToWorldPoint(mousePosition), 0.1f, results);
                foreach (var coll in results)
                {
                    arrow = coll.GetComponent<PathArrow>();
                    if (arrow)
                        break;
                }

                if (!arrow)
                    return;
                
                if (GameMetrics.Paths[_id == 0 ? 1 : 0] != arrow.Vector)
                    GameMetrics.Paths[_id] = arrow.Vector;
                else
                    return;

                DisableArrows();
            }
        }

        private void DisableArrows()
        {
            UpdateColorPaths();
                
            _customInput.InputMouse_Action -= MouseClick;
            _isArrow = false;
        }

        private void UpdateColorPaths()
        {
            foreach (var arrow in _arrows)
            {
                arrow.SetPath(GameMetrics.Paths.Contains(arrow.Vector));
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
