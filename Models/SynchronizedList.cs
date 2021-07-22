using System.Collections.Generic;

namespace FruitBasket.Models
{
    public class SynchronizedList<T>
    {
        private readonly List<T> _list;
        private static readonly object _lockObject;

        static SynchronizedList()
        {
            _lockObject = new object();
        }

        public SynchronizedList()
        {
            _list = new List<T>();
        }

        public bool TryAdd(T item)
        {
            lock(_lockObject)
            {
                if (_list.Contains(item))
                {
                    return false;
                }

                _list.Add(item);

                return true;
            }
        }
    }
}
