using System;
using System.IO;

namespace Priklad3
{
    /// <summary>
    /// Zdrojovy kod je podrobne okomentovany. Pouzil som dokumentacne XML komentare (tri lomky za sebou). 
    /// Tieto je mozne neskor exportovat, avsak hlavne maju zmysel pre tvorbu helpu (IntelliSense ich dokaze 
    /// zobrazit, takze vdaka tejto funkcionalite sa mozeme lepsie orientavat v takto okomentovanych 
    /// datovych typoch a metodach v zdrojovych kodoch).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Vytvorime instanciu (objekt) triedy ListDirectoriesFiles
            var listDirFiles = new ListDirectoriesFiles();

            // Zavolame metodu ParseParameters(), cim spracujeme vsetky argumenty
            listDirFiles.ParseParameters(args);

            // Po spracovani vypiseme vsetky priecinky a subory podla nastavenych parametrov
            listDirFiles.ShowDirectoriesAndFiles();
        }
    }
}
