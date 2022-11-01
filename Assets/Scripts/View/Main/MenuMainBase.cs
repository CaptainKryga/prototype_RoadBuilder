using UnityEngine;

namespace View.Main
{
    public abstract class MenuMainBase : MenuBase
    {
        private GameObject _panelBack;

        public void UsePanel(GameObject panelBack)
        {
            _panelBack = panelBack;
            SetEnable(true);
        }

        public void OnClick_Back()
        {
            SetEnable(false);
            _panelBack.SetActive(true);
        }
    }
}