using Model;
using UnityEngine;

namespace Controller
{
    public class GlobalController : MonoBehaviour
    {
        [SerializeField] private ModelController _modelController;
        
        private void Start()
        {
            _modelController.Restart();
        }
        
        public void Pause(bool flag)
        {
            
        }
    }
}