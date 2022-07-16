namespace Assets.Scripts
{
    public enum DieFace
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    public static class DieFaceHelper
    {
        public static int GetNumericValue(DieFace dieFace)
        {
            switch (dieFace)
            {
                case DieFace.One:
                    return 1;
                case DieFace.Two:
                    return 2;
                case DieFace.Three:
                    return 3;
                case DieFace.Four:
                    return 4;
                case DieFace.Five:
                    return 5;
                case DieFace.Six:
                    return 6;
                default:
                    return 0;
            }
        }
    }
}
