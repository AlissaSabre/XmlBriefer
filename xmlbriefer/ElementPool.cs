using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace xmlbriefer
{
    /// <summary>Pools information on observed elements.</summary>
    public class ElementPool
    {
        private readonly Dictionary<XName, ElementInfo> Pool = new Dictionary<XName, ElementInfo>();

        /// <summary>Gets information of an element by its name.</summary>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">Element of name specified by <paramref name="name"/> is not pooled.</exception>
        public ElementInfo this[XName name] => Pool[name];

        /// <summary>Number of elements in this pool.</summary>
        public int Count => Pool.Count;

        private readonly HashSet<XName> _Roots = new HashSet<XName>();

        /// <summary>List of observed root elements.</summary>
        public IEnumerable<XName> Roots => _Roots;

        /// <summary>
        /// Adds all elements in the provided document tree into this pool
        /// alongside of their information.
        /// </summary>
        /// <param name="root">The root element of the document tree.</param>
        public void AddElements(XElement root)
        {
            if (root is null) throw new ArgumentNullException(nameof(root));
            _Roots.Add(root.Name);
            AddElementsImpl(null, root);
        }

        private void AddElementsImpl(XName parent, XElement element)
        {
            var info = AddOrGetElementInfo(element.Name);
            if (parent != null)
            {
                info.AddParent(parent);
            }
            info.AddAttributes(element.Attributes().Select(a => a.Name));
            info.AddChildren(element.Elements().Select(e => e.Name));
            info.AddHasTextContents(element.Nodes().Any(n => n is XText t && !Utils.IsXmlWhiteSpace(t.Value)));

            foreach (var e in element.Elements())
            {
                AddElementsImpl(element.Name, e);
            }
        }

        private ElementInfo AddOrGetElementInfo(XName name)
        {
            if (!Pool.TryGetValue(name, out var info))
            {
                info = new ElementInfo(name);
                Pool.Add(name, info);
            }
            return info;
        }
    }
}
