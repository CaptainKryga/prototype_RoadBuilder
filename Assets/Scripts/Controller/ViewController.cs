using Model;
using UnityEngine;

namespace Controller
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private ChangeSetPath _changeSetPath;
        [SerializeField] private Evaluation _evaluation;
        public void Pause(bool flag)
        {
            Time.timeScale = flag ? 0 : 1;
        }

        public void SetPath(int id)
        {
            _changeSetPath.SetPath(id);
        }

        public void Result()
        {
            _dragAndDrop.enabled = false;
            _evaluation.Result();
        }
    }
}
