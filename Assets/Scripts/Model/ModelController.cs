using UnityEngine;

namespace Model
{
    public class ModelController : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;

        public void Restart()
        {
            _cameraController.Init();
        }
    }
}