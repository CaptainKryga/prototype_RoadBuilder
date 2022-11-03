using System;
using UnityEngine;

namespace Controller.CustomInput
{
    public abstract class CustomInputBase : MonoBehaviour
    {
        //isDownKey?, mousePosition
        public Action<Mouse, Vector2> InputMouse_Action;
        public Action<Vector2> InputMousePosition_Action;
        //keycode, isDownKey?
        public Action<KeyCode, bool> InputKeyboard_Action;
        
        public enum Mouse
        {
            Left,
            Right,
            ClickDown,
            ClickUp,
            Scroll
        }

        private void Update()
        {
            SetupKeyboard();
        }

        protected abstract void SetupKeyboard();
    }
}