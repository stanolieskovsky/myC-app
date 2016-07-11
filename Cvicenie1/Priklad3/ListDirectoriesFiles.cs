using System;
using System.IO;
using System.Security;

namespace Priklad3
{
    /// <summary>
    /// Umoznuje spracovat a vypisat subory a priecinky podla zadanych parametrov.
    /// </summary>
    public class ListDirectoriesFiles
    {
        #region Parameters

        /// <summary>
        /// Reprezentuje prepinac parametra pre rekurzivne prehliadanie.
        /// </summary>
        private class RecursiveSwitchParameter
        {
            /// <summary>
            /// Konstanta parametra.
            /// </summary>
            public const string Parameter = @"\r";

            /// <summary>
            /// Urcuje, ci bol parameter pouzity (predany programu).
            /// </summary>
            public bool WasUsed { get; set; }
            /// <summary>
            /// Maximalna uroven vnorenia.
            /// </summary>
            public int MaxLevel { get; set; }

            /// <summary>
            /// Konstruktor.
            /// </summary>
            public RecursiveSwitchParameter()
            {
                WasUsed = false;
                MaxLevel = int.MaxValue; // Konstanta najvacsieho celeho mozneho cisla (2147483647)
            }
        }

        /// <summary>
        /// Reprezentuje prepinac parametra pre zobrazenie stromovej struktury.
        /// </summary>
        private class TreeSwitchParameter
        {
            /// <summary>
            /// Konstanta parametra.
            /// </summary>
            public const string Parameter = @"\t";
            /// <summary>
            /// Konstanta pre prednastaveny pocet medzier.
            /// </summary>
            public const int DefaultSpaceCount = 2;

            /// <summary>
            /// Urcuje, ci bol parameter pouzity (predany programu).
            /// </summary>
            public bool WasUsed { get; set; }
            /// <summary>
            /// Pocet medzier.
            /// </summary>
            public int SpaceCount { get; set; }

            /// <summary>
            /// Konstruktor.
            /// </summary>
            public TreeSwitchParameter()
            {
                WasUsed = false;
                SpaceCount = DefaultSpaceCount;
            }

            /// <summary>
            /// Vytvori retazec zlozeny z medzier o pocte urcenom vlastnostou SpaceCount.
            /// </summary>
            /// <returns>Retazec medzier.</returns>
            public string GetSpacesString()
            {
                if (WasUsed)
                    return new string(' ', SpaceCount);
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Reprezentuje prepinac parametra pre zobrazenie suborov modrou farbou.
        /// </summary>
        private class ColorSwitchParameter
        {
            /// <summary>
            /// Konstanta parametra.
            /// </summary>
            public const string Parameter = @"\c";
            /// <summary>
            /// Konstanta pre prednastavenu farbu.
            /// </summary>
            public const ConsoleColor Color = ConsoleColor.Blue;

            /// <summary>
            /// Urcuje, ci bol parameter pouzity (predany programu).
            /// </summary>
            public bool WasUsed { get; set; }

            /// <summary>
            /// Konstruktor.
            /// </summary>
            public ColorSwitchParameter()
            {
                WasUsed = false;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Cesta k priecinku.
        /// </summary>
        private string _path;
        /// <summary>
        /// Prepinac pre rekurzivne prehliadanie priecinkov.
        /// </summary>
        private readonly RecursiveSwitchParameter _recursiveParam = new RecursiveSwitchParameter();
        /// <summary>
        /// Prepinac pre stromove hierarchicke zobrazenie priecinkov a suborov.
        /// </summary>
        private readonly TreeSwitchParameter _treeParam = new TreeSwitchParameter();
        /// <summary>
        /// Prepinac pre farebne zobrazenie suborov.
        /// </summary>
        private readonly ColorSwitchParameter _colorParam = new ColorSwitchParameter();

        #endregion

        #region Methods

        /// <summary>
        /// Spracuje parametre predane programu.
        /// </summary>
        /// <param name="args">Pole argumentov.</param>
        public void ParseParameters(string[] args)
        {
            // Ak pole obsahuje nejake polozky, prva z nich (args[0]) by mal byt priecinok; 
            // skontrolujeme preto, ci priecinok existuje - ak ano, ulozime si ho do premennej 
            // a pokracujeme v spracovavani argumentov
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                // Nastavi sa cesta na zadany priecinok
                _path = args[0];

                // Prejdeme vsetky zadane argumenty (ideme od jednotky, lebo na nultom prvku 
                // pola je cesta k priecinku)
                for (int i = 1; i < args.Length; i++)
                {
                    // Ak najdeme jeden z troch parametrov, nastavime jeho vlastnosti, pricom 
                    // ak sa vyzaduje dalsi volitelny parameter "cislo", tento ihned ziskame
                    switch (args[i])
                    {
                        case RecursiveSwitchParameter.Parameter:
                            _recursiveParam.WasUsed = true;
                            // Ak existuje nasledujuci parameter, otestujeme, ci je to cislo (mozeme ho bud skonvertovat a zistit, 
                            // ci nenastala vynimka - pouzit metodu TryParse alebo napr. mozeme zistit, ci prvy znak je znakom cisla,
                            // na tento ucel mozeme pouzit metodu IsDigit() triedy char, ktora vracia hodnotu true/false podla toho, 
                            // ci vstupny znak je znak cisla ('0'..'9') alebo nie
                            if (i + 1 < args.Length && char.IsDigit(args[i + 1][0]))
                            {
                                _recursiveParam.MaxLevel = int.Parse(args[i + 1]);
                                i++;
                            }
                            break;

                        case TreeSwitchParameter.Parameter:
                            _treeParam.WasUsed = true;
                            if (i + 1 < args.Length && char.IsDigit(args[i + 1][0]))
                            {
                                _treeParam.SpaceCount = int.Parse(args[i + 1]);
                                i++;
                            }
                            break;

                        case ColorSwitchParameter.Parameter:
                            _colorParam.WasUsed = true;
                            break;

                        default:
                            Console.WriteLine("Invalid parameter \"{0}\".", args[i]);
                            break;
                    }
                }
            }
            else // Ak nebol predany ziaden parameter alebo priecinok neexistuje
            {
                // Nastavi aktualny priecinok (miesto, z ktoreho je spusteny exe subor)
                _path = Directory.GetCurrentDirectory();
            }
        }

        /// <summary>
        /// Zobrazi vsetky priecinky a subory podla nastavenych parametrov.
        /// </summary>
        public void ShowDirectoriesAndFiles()
        {
            ListDirectoriesAndFiles(_path, 0, string.Empty);
        }

        /// <summary>
        /// Zobrazi vsetky priecinky a subory podla nastavenych parametrov.
        /// </summary>
        /// <param name="path">Cesta k priecinku.</param>
        /// <param name="currentLevel">Aktualna uroven prehliadania.</param>
        /// <param name="spaces">Pocet medzier na odsadenie priecinkov a suborov podla hierachie.</param>
        private void ListDirectoriesAndFiles(string path, int currentLevel, string spaces)
        {
            // Pouzita konstrukcia try-catch na odchytenie vynimiek kvoli nedostatocnym pravam na prehliadanie vsetkych priecinkov
            try
            {
                // Vytvorime objekt triedy DirectoryInfo reprezentuju priecinok
                var di = new DirectoryInfo(path);
                // Ziskame vsetky priecinky nacahadzajuce sa v priecinku di.FullName
                DirectoryInfo[] directories = di.GetDirectories();
                // Prejdeme vsetky priecinky a tieto vypiseme
                foreach (var directory in directories)
                {
                    // Vypisujeme v tvare datum (den.mesiac.rok hodina:minuta, medzery, nazov priecinku)
                    Console.WriteLine("{0:dd}.{0:MM}.{0:yyyy} {0:hh}:{0:mm} {1,15} {2} {3}\\", directory.CreationTime, "", spaces, directory.Name);

                    // Ak bol nastaveny parameter pre rekurzivne prehladavanie a ak je uroven vnorenia mensia nez maximalna,
                    // zavolame metodu samu sebou (rekurzivne)
                    if (_recursiveParam.WasUsed && currentLevel < _recursiveParam.MaxLevel)
                        ListDirectoriesAndFiles(directory.FullName, currentLevel + 1,
                            _treeParam.WasUsed ? spaces + _treeParam.GetSpacesString() : spaces);
                }

                // Zapamatame si aktualnu farbu pisma
                ConsoleColor originColor = Console.ForegroundColor;
                // Ak bol nastaveny parameter pre zmenu farby oznacenia suborov, nastavime farbu
                if (_colorParam.WasUsed)
                    Console.ForegroundColor = ColorSwitchParameter.Color;

                // Ziskame vsetky subory, ktore obsahuje priecinok di.FullName
                FileInfo[] files = di.GetFiles();
                // Prejdeme vsetky subory a vypiseme ich
                foreach (var file in files)
                {
                    Console.WriteLine("{0:dd}.{0:mm}.{0:yyyy} {0:hh}:{0:mm} {1,15} {2} {3}",
                        file.CreationTime, file.Length, spaces, file.Name);
                }

                // Ak bola nastavena farba, vratime ju do povodneho stavu
                if (_colorParam.WasUsed)
                    Console.ForegroundColor = originColor;
            }
            catch (SecurityException)
            {
                // Zamlcime; nie vzdy je spravne zamlcanie, mali by sme sa mu vyhybat alebo aspon logovat.
                // Iny sposob je sirit vynimku pomocou klucoveho slova throw, tu ale naozaj chcem zamlcat vynimku. :)
            }
            catch (UnauthorizedAccessException)
            {
                // Opat zamlcime
            }
        }

        #endregion
    }
}
