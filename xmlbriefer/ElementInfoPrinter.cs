using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace xmlbriefer
{
    /// <summary>Prints information of elements in a pool.</summary>
    public class ElementInfoPrinter
    {
        private const string TextNodeLabel = "#text";

        private readonly ElementPool Pool;

        /// <summary>Creates an instance of the printer.</summary>
        /// <param name="pool">The pool this printer prints from.</param>
        public ElementInfoPrinter(ElementPool pool)
        {
            Pool = pool;
        }

        /// <summary>Prints the elements in the pool.</summary>
        public void Print()
        {
            var done = new HashSet<XName>(Pool.Count);
            foreach (var root in Pool.Roots)
            {
                PrintImpl(root, 0, done);
            }
        }

        private void PrintImpl(XName element, int level, HashSet<XName> done)
        {
            Indent(level);
            WriteName(element);
            if (done.Contains(element))
            {
                Console.WriteLine(" =");
            }
            else
            {
                Console.WriteLine();
                done.Add(element);
                var current = Pool[element];
                // TODO: print some auxiliary information on the current element here.
                foreach (var attribute in current.Attributes)
                {
                    Indent(level + 1);
                    Console.Write("@");
                    WriteName(attribute);
                    Console.WriteLine();
                }
                if (current.HasTextContents)
                {
                    Indent(level + 1);
                    Console.WriteLine(TextNodeLabel);
                }
                foreach (var child in current.Children)
                {
                    PrintImpl(child, level + 1, done);
                }
            }
        }

        private readonly char[] Spacer = "                    ".ToCharArray();

        private void Indent(int level)
        {
            level *= 4;

            while (level >= Spacer.Length)
            {
                Console.Write(Spacer);
                level -= Spacer.Length;
            }
            if (level > 0)
            {
                Console.Write(Spacer, 0, level);
            }
        }

        private void WriteName(XName name)
        {
            Console.Write(name.LocalName);
            if (name.Namespace != XNamespace.None)
            {
                Console.Write("{0}{1}{2}", '{', name.NamespaceName, '}');
            }
        }
    }
}
