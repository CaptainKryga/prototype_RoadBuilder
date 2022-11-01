using UnityEngine;

namespace Model
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GenerateLevel _generateLevel;
        
        [SerializeField] private float _speed;
        
        private Camera _camera;
        private float _xMax, _yMax;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Init()
        {
            _xMax = GameMetrics.SizeSquare * _generateLevel.SizeMap.x;
            _yMax = GameMetrics.SizeSquare * _generateLevel.SizeMap.y;
            _camera.transform.position = new Vector3(_xMax / 2, _yMax / 2, -10);
        }

        private void Update()
        {
            float xDelta = Input.GetAxis("Horizontal");
            float zDelta = Input.GetAxis("Vertical");
            
            if (xDelta != 0f || zDelta != 0f)
            {
                Vector3 direction = _camera.transform.localRotation * new Vector3(xDelta, zDelta, 0).normalized;
                float distance = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta)) * Time.deltaTime * _speed;

                Vector3 position = _camera.transform.localPosition;
                position += direction * distance;
                
                position.x = Mathf.Clamp(position.x, 0f, _xMax);
                position.y = Mathf.Clamp(position.y, 0f, _yMax);

                _camera.transform.localPosition = position;
            }
        }
    }
}