using System;
using System.IO;
using System.Xml.Linq;

namespace xmlbriefer
{
    class Program
    {
        static void Main(string[] args)
        {
            var pool = new ElementPool();
            foreach (var pattern in args)
            {
                foreach (var filename in Utils.ExpandPattern(pattern))
                {
                    var xml = XElement.Load(filename);
                    pool.AddElements(xml);
                }
            }
            var printer = new ElementInfoPrinter(pool);
            printer.Print();
        }
    }
}
