using UnityEngine;

namespace Model.Components
{
    public class CellDynamic : Entity
    {
        public Transform[] Points;
        public override void Setup(byte type)
        {
            Type = type;
            Debug.text = type.ToString();
            IsStatic = false;
        }
    }
}
