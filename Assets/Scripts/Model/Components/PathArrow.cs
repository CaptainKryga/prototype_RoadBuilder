using UnityEngine;

namespace Model
{
    public class PathArrow : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public Vector3 Vector;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetPath(bool flag)
        {
            _spriteRenderer.color = flag ? Color.green : Color.white;
        }
    }
}
