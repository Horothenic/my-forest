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
    
    public class OrderedList<T> : IList<T>
    {
        private readonly IComparer<T> _comparer;
        private readonly List<T> _innerList = new List<T>();

        public OrderedList() : this(Comparer<T>.Default) { }

        public OrderedList(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public T this[int index]
        {
            get => _innerList[index];
            set => throw new NotSupportedException("Cannot set an indexed item in an ordered list.");
        }

        public int Count => _innerList.Count;
        public bool IsReadOnly => false;
        
        public void Add(T item)
        {
            var index = _innerList.BinarySearch(item, _comparer);
            index = (index >= 0) ? index : ~index;
            _innerList.Insert(index, item);
        }

        public T PopAt(int index)
        {
            var item = _innerList[index];
            _innerList.RemoveAt(index);
            return item;
        }

        public void Clear() => this._innerList.Clear();
        public bool Contains(T item) => this._innerList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => this._innerList.GetEnumerator();
        public int IndexOf(T item) => this._innerList.IndexOf(item);
        public void Insert(int index, T item) => throw new NotSupportedException("Cannot insert an indexed item in an ordered list.");
        public bool Remove(T item) => this._innerList.Remove(item);
        public void RemoveAt(int index) => this._innerList.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
