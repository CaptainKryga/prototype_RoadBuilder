using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.CustomInput
{
    public class CustomInputGame : CustomInputBase
    {
        protected override void SetupKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.currentSelectedGameObject)
                InputMouse_Action?.Invoke(true, Input.mousePosition);
        }
    }
}
