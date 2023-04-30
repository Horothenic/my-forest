namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void AddTo<T>(this T element, ICollection<T> collection)
        {
            if (element == null) return;
            
            collection.Add(element);
        }
    }
}
