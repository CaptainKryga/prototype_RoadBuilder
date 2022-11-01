using UnityEngine;

namespace View
{
    public abstract class MenuBase : MonoBehaviour
    {
        [SerializeField] protected GameObject PanelBase;

        protected void SetEnable(bool enable)
        {
            PanelBase.SetActive(enable);
        }
    }
}