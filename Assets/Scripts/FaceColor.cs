using UnityEngine;

namespace Assets.Scripts
{
    public enum FaceColor
    {
        Red,
        Green,
        Blue
    }

    public static class FaceColorHelper
    {
        public static Color GetColorFromFaceColor(FaceColor faceColor)
        {
            switch (faceColor)
            {
                case FaceColor.Red:
                    return Color.red;
                case FaceColor.Green:
                    return Color.green;
                case FaceColor.Blue:
                    return Color.blue;
                default:
                    return Color.gray;
            }
        }
    }
}