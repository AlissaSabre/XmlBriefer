using System;
using System.Xml.Linq;

namespace xmlbriefer
{
    class Program
    {
        static void Main(string[] args)
        {
            var pool = new ElementPool();
            foreach (var filename in args)
            {
                var xml = XElement.Load(filename);
                pool.AddElements(xml);
            }
            var printer = new ElementInfoPrinter(pool);
            printer.Print();
        }
    }
}
