using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLibrary
{
    /// <summary>
    /// Obojsmerne zretazeny zoznam.
    /// </summary>
    /// <typeparam name="T">Typ.</typeparam>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Hlava, vytvorena pre ulahcenie prace s obojsmerne zretazenym zoznamom. 
        /// </summary>
        private readonly DoublyLinkedNode<T> _head;

        /// <summary>
        /// Pocet prvkov.
        /// </summary>
        public int Count { get; private set; }
        
        /// <summary>
        /// Vytvori objekt obojsmerne zretazeneho zoznamu.
        /// </summary>
        public DoublyLinkedList()
        {
            Count = 0;

            _head = new DoublyLinkedNode<T>(default(T));
            _head.Previous = _head;
            _head.Next = _head;
        }

        /// <summary>
        /// Prida prvok do zoznamu.
        /// </summary>
        /// <param name="item">Prvok, ktory sa prida do zoznamu.</param>
        public void Add(T item)
        {
            var node = new DoublyLinkedNode<T>(item);
            node.Next = _head;
            node.Previous = _head.Previous;

            _head.Previous.Next = node;
            _head.Previous = node;

            Count++;
        }

        /// <summary>
        /// Odstrani prvok zo zoznamu.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            var node = Find(item);
            if (node == null)
                return false;

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            node.Next = null;
            node.Previous = null;

            Count--;

            return true;
        }

        /// <summary>
        /// Vymaze zoznam.
        /// </summary>
        public void Clear()
        {
            _head.Next = _head;
            _head.Previous = _head;

            Count = 0;
        }

        /// <summary>
        /// Zisti, ci zoznam obsahuje prvok.
        /// </summary>
        /// <param name="item">Prvok.</param>
        /// <returns>True, ak sa nachadza v zozname. Inak vrati false.</returns>
        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        /// <summary>
        /// Vyhlada prvok v zozname.
        /// </summary>
        /// <param name="item">Prvok.</param>
        /// <returns>Odkaz na uzol zoznamu, ktory obsahuje hladany prvok.</returns>
        private DoublyLinkedNode<T> Find(T item)
        {
            for (var node = _head.Next; node != _head; node = node.Next)
            {
                if (Equals(node.Data, item))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Skopiruje obsah zoznamu do pola od indexu <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">Pole, ktoreho velkost by malo byt vacsie alebo rovne ako pocet prvkov zoznamu.</param>
        /// <param name="arrayIndex">Index do pola, do ktoreho sa zacnu prvky zoznamu zapisovat.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than zero.");

            if (Count > array.Length - arrayIndex)
                throw new ArgumentException(
                    "The number of elements in the source is greater than the available space from index to the end of the destination array.");

            int idx = 0;
            for (var node = _head.Next; node != _head; node = node.Next)
                array[arrayIndex + idx++] = node.Data;
        }

        // Vytvoreny indexer - nie velmi stastne riesenie pre pouzitie, ak sa bude pouzivat v cykle for, 
        // vidime, ze v kode indexera je for, takze cyklus v cykle - pomale! Lepsie bude pouzit foreach.
        // Ten, aby sme mohli pouzit, implementujeme rozhranie IEnumerable<T>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                int idx = 0;
                for (var node = _head.Next; node != _head; node = node.Next)
                {
                    if (index == idx)
                    {
                        return node.Data;
                    }

                    idx++;
                }

                throw new Exception("Something is wrong. This line should be unreachable.");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            // Pouzitie cez enumerator
            //return new DoublyLinkedEnumerator<T>(_head);

            // Pouzitie cez iterator - enumerator je generovany automaticky kompilatorom
            for (var node = _head.Next; node != _head; node = node.Next)
            {
                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Priklad pouzitia metody, ktora pouziva iterator cez yield - opat sa generuje enumerator automaticky
        public IEnumerable<T> GetReverse()
        {
            for (var node = _head.Previous; node != _head; node = node.Previous)
            {
                yield return node.Data;
            }
        }
    }
}
