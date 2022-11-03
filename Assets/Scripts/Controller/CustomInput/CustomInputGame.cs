using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller.CustomInput
{
    public class CustomInputGame : CustomInputBase
    {
        protected override void SetupKeyboard()
        {
            if (EventSystem.current.currentSelectedGameObject)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                InputMouse_Action?.Invoke(Mouse.ClickDown, Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse0))
                InputMouse_Action?.Invoke(Mouse.ClickUp, Input.mousePosition);
            
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
                InputMouse_Action?.Invoke(Mouse.Scroll, new Vector2(Input.GetAxis("Mouse ScrollWheel"), 0));
            
            InputMousePosition_Action?.Invoke(Input.mousePosition);
        }
    }
}
