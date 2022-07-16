using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DieFaceMapping")]
    public class DieFaceMapping : ScriptableObject
    {
        [SerializeField] private Sprite _fallbackSprite;
        [NonReorderable] [SerializeField] private GenericDictionary<DieFace, Sprite> _sprites;

        public Sprite GetSprite(DieFace dieFace)
        {
            if (_sprites.TryGetValue(dieFace, out Sprite sprite))
            {
                return sprite;
            }

            return _fallbackSprite;
        }
    }
}
