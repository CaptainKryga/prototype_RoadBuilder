using System;
using UnityEngine;

namespace Controller.CustomInput
{
    public abstract class CustomInputBase : MonoBehaviour
    {
        //isDownKey?, mousePosition
        public Action<bool, Vector2> InputMouse_Action;
        //keycode, isDownKey?
        public Action<KeyCode, bool> InputKeyboard_Action;

        private void Update()
        {
            SetupKeyboard();
        }

        protected abstract void SetupKeyboard();
    }
}