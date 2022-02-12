using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace xmlbriefer
{
    /// <summary>Information about an XML element.</summary>
    public class ElementInfo
    {
        /// <summary>Creates an instance.</summary>
        /// <param name="name">Name of this element.</param>
        public ElementInfo(XName name)
        {
            Name = name;
        }

        /// <summary>Name of this element.</summary>
        public XName Name { get; }

        private HashSet<XName> _Attributes = new HashSet<XName>();

        /// <summary>Observed attributes of this elements.</summary>
        public IEnumerable<XName> Attributes => _Attributes;

        private readonly HashSet<XName> _Parents = new HashSet<XName>();

        /// <summary>Observed parent elements.</summary>
        public IEnumerable<XName> Parents => _Parents;

        private readonly HashSet<XName> _Children = new HashSet<XName>();

        /// <summary>Observed child elements.</summary>
        public IEnumerable<XName> Children => _Children;

        /// <summary>Whether a significant text node is observed in this element's contents.</summary>
        public bool HasTextContents { get; private set; }

        /// <summary>Adds a list of observed attributes in a single instance of this element.</summary>
        /// <param name="attributes">A list of observed attributes.</param>
        public void AddAttributes(IEnumerable<XName> attributes)
        {
            _Attributes.UnionWith(attributes);
        }

        /// <summary>Adds an element as an observed parent of this element.</summary>
        /// <param name="parent">An observed parent.</param>
        public void AddParent(XName parent)
        {
            _Parents.Add(parent);
        }

        /// <summary>Adds a list of observed child elements in a single instance of this element.</summary>
        /// <param name="children">A list of observed child elements.</param>
        public void AddChildren(IEnumerable<XName> children)
        {
            _Children.UnionWith(children);
        }

        /// <summary>Adds whether a significant text node is observed in this element's contents.</summary>
        /// <param name="has">True when a text node is observed. False otherwise.</param>
        /// <remarks>
        /// Insignificant text nodes, i.e.,
        /// those consisting only of white space characters as defined by XML specification,
        /// should be ignored beforehand by the caller of this method.
        /// </remarks>
        public void AddHasTextContents(bool has)
        {
            HasTextContents |= has;
        }
    }
}
