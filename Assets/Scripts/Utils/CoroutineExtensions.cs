using System.Collections;

namespace Assets.Scripts.Utils
{
    public static class CoroutineExtensions
    {
        public static IEnumerator GetResult<T>(this IEnumerator enumerator, CoroutineResult<T> valueWrapper) where T : class
        {
            yield return enumerator;

            valueWrapper.Value = enumerator.Current as T;
        }
    }
}
