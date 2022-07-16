using UnityEngine;

namespace Assets.Scripts
{
    public enum DieFace
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6
    }

    public static class DieFaceHelper
    {
        static DieFaceHelper()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
        }

        public static int GetNumericValue(DieFace dieFace)
        {
            return (int)dieFace;
        }

        public static DieFace Roll()
        {
            return (DieFace)Random.Range(1, 6);
        }
    }
}
