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
        [SerializeField] private Color _mutedColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);
        [SerializeField] private float _mutedColorStrength = 0.75f;

        public Color GetColorFromFaceColor(FaceColor faceColor)
        {
            return GetColorFromFaceColor(faceColor, false);
        }

        public Color GetColorFromFaceColor(FaceColor faceColor, bool mute)
        {
            Color value;

            switch (faceColor)
            {
                case FaceColor.Red:
                    value = _red;
                    break;
                case FaceColor.Green:
                    value = _green;
                    break;
                case FaceColor.Blue:
                    value = _blue;
                    break;
                default:
                    value = _gray;
                    break;
            }

            if (mute)
            {
                value = Color.Lerp(value, _mutedColor, _mutedColorStrength);
            }

            return value;
        }
    }
}
