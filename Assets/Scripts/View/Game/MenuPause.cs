using Controller;
using UnityEngine;

namespace View.Game
{
    public class MenuPause : MenuGameBase
    {
        [SerializeField] private GlobalController _globalController;

        public override void UsePanel()
        {
            SetEnable(true);
        }

        public void OnClick_SetPause()
        {
            PanelBase.SetActive(!PanelBase.activeSelf);
            _globalController.Pause(PanelBase.activeSelf);
        }
    }
}