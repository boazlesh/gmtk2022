using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/FaceColorMapping")]
    public class FaceColorMapping : ScriptableObject
    {
        [SerializeField] private Color _red;
        [SerializeField] private Color _green;
        [SerializeField] private Color _blue;
        [SerializeField] private Color _gray;

        public Color GetColorFromFaceColor(FaceColor faceColor)
        {
            switch (faceColor)
            {
                case FaceColor.Red:
                    return _red;
                case FaceColor.Green:
                    return _green;
                case FaceColor.Blue:
                    return _blue;
                default:
                    return _gray;
            }
        }
    }
}
