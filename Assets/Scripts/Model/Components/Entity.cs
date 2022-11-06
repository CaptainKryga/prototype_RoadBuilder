using UnityEngine;

namespace Model.Components
{
    public abstract class Entity : MonoBehaviour
    {
        public TMPro.TMP_Text Debug;
        public byte Type;
        public bool IsStatic;

        public abstract void Setup(byte type);
    }
}
