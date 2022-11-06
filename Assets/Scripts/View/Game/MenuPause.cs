using Controller;
using UnityEngine;

namespace View.Game
{
    public class MenuPause : MenuGameBase
    {
        [SerializeField] private ViewController _view;

        public override void UsePanel()
        {
            SetEnable(true);
        }

        public void OnClick_SetPause()
        {
            PanelBase.SetActive(!PanelBase.activeSelf);
            _view.Pause(PanelBase.activeSelf);
        }
    }
}