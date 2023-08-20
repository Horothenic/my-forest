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

        public static List<T> Shuffle<T>(this List<T> list)
        {
            return list.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
