namespace Model.Components
{
    public class CellStatic : Entity
    {
        public override void Setup(byte type)
        {
            Type = type;
            Debug.text = type.ToString();
            IsStatic = true;
        }
    }
}
