using System;
using System.Globalization;

namespace Task1
{
    /// <summary>
    /// Trieda Person. // Trieda moze mat 2 pristupove modifikatory: 
    ///  - internal (implicitne, t.j. ak sa neuvedie ziadny) - tzn. verejny v ramci zostavenia (assembly), 
    ///    sukromny mimo neho (ine zostavenie nema k triede pristup, nie je ho vidno)
    ///  - public - verejny, mozno potom triedu pouzivat nielen v tomto zostaveni, 
    ///    ale aj mimo neho (v inych zostaveniach - projektoch). 
    /// </summary>
    public class Person : IComparable, IComparable<Person>
    {
        /// <summary>
        /// Meno. // Priklad datoveho clena (field). Ak sa pristupovy modifikator neuvedie, implicitne je private. 
        /// Ine pristupove modifikatory okrem private: protected, public, internal, protected internal.  
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Meno. // Priklad klasickej full property, zapuzdruje datovy clen <see cref="_firstName"/>. // pouzitie XML znacky s _firstName 
        /// umozni automaticky zmenit nazov, ak pouzijeme premenovanie pomocou refaktoringu.
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
       
        /// <summary>
        /// Priezvisko. // Priklad auto property
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Datum narodenia.
        /// </summary>
        public DateTime Birthday { get; set; } // Ak by sme zapisali "Birthday { get; private set; }", bude Birthday z vonku len na citanie, zapisovat by sa mohlo len z vnutra - vyskusajte si

        /// <summary>
        /// Vek, vypocitavany z <see cref="Birthday"/> a aktualneho casu. // Vlastnost iba na citanie (read-only)
        /// </summary>
        public int Age
        {
            get { return (int)((DateTime.Now - Birthday).TotalDays / 365); } // Iba getter, setter sme odstranili
        }

        /// <summary>
        /// Vytvori objekt typu <see cref="Person"/>. // Bezparametricky konstruktor.
        /// </summary>
        public Person()
        {
        }

        /// <summary>
        /// Vytvori objekt typu <see cref="Person"/>. // Parametricky konstruktor.
        /// </summary>
        /// <param name="firstName">Meno.</param>
        /// <param name="lastName">Priezvisko.</param>
        /// <param name="birthday">Datum narodenia.</param>
        public Person(string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        /// <summary>
        /// Vrati retazec v tvare Meno Priezvisko [Vek].
        /// </summary>
        /// <returns>Retazec v tvare Meno Priezvisko [Vek].</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} [{2}]", FirstName, LastName, Age);
        }

        protected bool Equals(Person other)
        {
            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && Birthday.Equals(other.Birthday);
        }

        // Equals sme si prekryli, pretoze ho chceme pouzit na porovnavanie pomocou hodnot objektu. Ak by sme ho neprekryli, objekty porovnava na zaklade referencii (odkazov).
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Person)obj);
        }

        // S Equals je nutne prekryvat aj GetHashcode, dolezity je pre mnohe triedy, ktore pracuju s hashom, napr. v Dictionary<K, T>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Birthday.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Porovnava aktualny objekt s inym objektom daneho typu. Ak <paramref name="obj"/> nie je objekt typu <see cref="Person"/>, vyhodi vynimku. 
        /// // Vsimnite si, toto je explicitna definicia metody CompareTo() negenerickeho rozhrania IComparable.
        /// </summary>
        /// <param name="obj">Objekt typu <see cref="Person"/>.</param>
        /// <returns>Vracia bud cislo &lt; 0, ak objekt je mensi nez <paramref name="obj"/>. Nulu, ak sa objekty rovnaju.
        /// Ak je objekt vacsi nez <paramref name="obj"/>, vrati cislo &gt; 0.</returns>
        /// <exception cref="ArgumentException">Vyhodi vynimku, ak parameter <paramref name="obj"/> nie je typu <see cref="Person"/>.</exception> 
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var other = obj as Person;
            if (other != null)
                return CompareTo(other);

            throw new ArgumentException("Object is not a Person.");
        }

        /// <summary>
        /// Porovnava aktualny objekt s inym objektom daneho typu.
        /// // Vsimnite si, toto je implicitna definicia metody CompareTo() generickeho rozhrania IComparable(T).
        /// </summary>
        /// <param name="other">Objekt typu <see cref="Person"/>.</param>
        /// <returns>Vracia bud cislo &lt; 0, ak objekt je mensi nez <paramref name="other"/>. Nulu, ak sa objekty rovnaju.
        /// Ak je objekt vacsi nez <paramref name="other"/>, vrati cislo &gt; 0.</returns>
        public int CompareTo(Person other)
        {
            if (other == null)
                return 1;

            //return Age.CompareTo(other.Age);

            var result = string.Compare(LastName, other.LastName, StringComparison.InvariantCulture);
            if (result != 0)
                return result;

            result = string.Compare(FirstName, other.FirstName, StringComparison.InvariantCulture);
            if (result != 0)
                return result;

            result = DateTime.Compare(Birthday, other.Birthday) * -1; // -1 kvoli zmene znamienka, aby sa porovnavalo od najmladsieho po najstarsieho
            return result;
        }

        /// <summary>
        /// Priklad indexera. V tejto triede velmi nema zmysel, no pre ukazku si ho vyskusajme. 
        /// </summary>
        /// <param name="index">0 spristupni <see cref="FirstName"/>, 1 spristupni <see cref="LastName"/> a 2 <see cref="Age"/>.</param>
        /// <returns>Retazec podla daneho indexu.</returns>
        /// <exception cref="IndexOutOfRangeException">Vyhodi, ak je index rozny ako 0, 1 a 2. :)</exception>
        /// <exception cref="InvalidOperationException">Vyhodi pri pokuse zmenit <see cref="Age"/>.</exception>
        public string this[int index]
        {
            get // getter - vracia hodnoty
            {
                switch (index)
                {
                    case 0:
                        return FirstName;
                    case 1:
                        return LastName;
                    case 2:
                        return Age.ToString(CultureInfo.InvariantCulture);
                    default:
                        throw new IndexOutOfRangeException("index");
                }
            }
            set // setter - nastavuje hodnoty cez klucove slovo value
            {
                switch (index)
                {
                    case 0:
                        FirstName = value;
                        break;
                    case 1:
                        LastName = value;
                        break;
                    case 2:
                        throw new InvalidOperationException("Age cannot be changed.");
                    default:
                        throw new IndexOutOfRangeException("index");
                }
            }
        }

        /// <summary>
        /// Priklad dalsieho indexera - funguje tu overloading. V tejto triede velmi nema zmysel, no pre ukazku si ho vyskusajme. 
        /// </summary>
        /// <param name="name">FirstName spristupni <see cref="FirstName"/>, LastName spristupni <see cref="LastName"/> a Age <see cref="Age"/>.</param>
        /// <returns>Retazec podla daneho indexu.</returns>
        /// <exception cref="IndexOutOfRangeException">Vyhodi, ak je index rozny ako FirstName, LastName alebo Age. :)</exception>
        /// <exception cref="InvalidOperationException">Vyhodi pri pokuse zmenit <see cref="Age"/>.</exception>
        public string this[string name]
        {
            get // getter - vracia hodnoty
            {
                switch (name)
                {
                    case "FirstName":
                        return FirstName;
                    case "LastName":
                        return LastName;
                    case "Age":
                        return Age.ToString(CultureInfo.InvariantCulture);
                    default:
                        throw new IndexOutOfRangeException("index");
                }
            }
            set // setter - nastavuje hodnoty cez klucove slovo value
            {
                switch (name)
                {
                    case "FirstName":
                        FirstName = value;
                        break;
                    case "LastName":
                        LastName = value;
                        break;
                    case "Age":
                        throw new InvalidOperationException("Age cannot be changed.");
                    default:
                        throw new IndexOutOfRangeException("index");
                }
            }
        }
    }
}
