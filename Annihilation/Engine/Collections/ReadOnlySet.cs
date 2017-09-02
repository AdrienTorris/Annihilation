using System.Collections;
using System.Collections.Generic;

namespace Engine.Collections
{
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
        bool Contains(T element);
    }

    public class ReadOnlySet<T> : IReadOnlySet<T>
    {
        private readonly ISet<T> _set;

        public int Count => _set.Count;

        public ReadOnlySet(ISet<T> set) => _set = set;

        public bool Contains(T item) => _set.Contains(item);

        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _set.GetEnumerator();
    }
}