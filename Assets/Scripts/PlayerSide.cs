using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSide : MonoBehaviour
    {
        [SerializeField] private Color _color;

        private void OnValidate()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.color = _color;
            }
        }
    }
}
