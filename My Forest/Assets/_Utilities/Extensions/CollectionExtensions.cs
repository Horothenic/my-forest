using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void AddTo<T>(this T element, ICollection<T> collection)
        {
            if (element == null) return;

            collection.Add(element);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable = enumerable.OrderBy(x => UnityEngine.Random.value);
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            return list = list.OrderBy(x => UnityEngine.Random.value).ToList();
        }
    }
}
