namespace MyLibrary
{
    /// <summary>
    /// Uzol obojsmerne zretazeneho zoznamu.
    /// </summary>
    /// <typeparam name="T">Typ prvku, ktory bude uzol obsahovat v <see cref="Data"/>.</typeparam>
    class DoublyLinkedNode<T>
    {
        /// <summary>
        /// Odkaz na predchadzajuci uzol.
        /// </summary>
        public DoublyLinkedNode<T> Previous { get; set; }
        
        /// <summary>
        /// Odkaz na nasledujuci uzol.
        /// </summary>
        public DoublyLinkedNode<T> Next { get; set; }
        
        /// <summary>
        /// Prvok typu T.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Vytvori uzol pre obojsmerne zretazeny zoznam.
        /// </summary>
        /// <param name="data">Prvok, ktory bude uzol obsahovat.</param>
        public DoublyLinkedNode(T data)
        {
            Data = data;
        }
    }
}
