using System.Collections;
using System.Collections.Generic;

namespace MyLibrary
{
    class DoublyLinkedEnumerator<T> : IEnumerator<T>
    {
        private readonly DoublyLinkedNode<T> _head;
        private DoublyLinkedNode<T> _current;

        public DoublyLinkedEnumerator(DoublyLinkedNode<T> head)
        {
            _current = _head = head;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _current = _current.Next;
            return _current != _head;
        }

        public void Reset()
        {
            _current = _head;
        }

        public T Current { get { return _current.Data; } }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
