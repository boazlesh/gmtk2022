using UnityEngine;

public enum FaceColor
{
    Red,
    Green,
    Blue
}

public static class FaceColorHelper
{
    private static readonly Color _red = new Color(0.6f, 0.1f, 0.1f);
    private static readonly Color _green = new Color(0.1f, 0.6f, 0.1f);
    private static readonly Color _blue = new Color(0.1f, 0.1f, 0.6f);
    private static readonly Color _gray = new Color(0.1f, 0.1f, 0.1f);

    public static Color GetColorFromFaceColor(FaceColor faceColor)
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
