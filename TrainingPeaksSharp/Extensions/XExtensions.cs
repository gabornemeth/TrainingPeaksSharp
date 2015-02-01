using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrainingPeaksSharp.Extensions
{
    /// <summary>
    /// LINQ to XML extensions
    /// </summary>
    static class XExtensions
    {
        public static IEnumerable<XElement> GetDescendants(this XElement element, string localName)
        {
            return from child in element.Descendants() where child.Name.LocalName == localName select child;
        }

        public static XElement GetFirstDescendant(this XElement element, string localName)
        {
            foreach (var child in element.Descendants())
            {
                if (child.Name.LocalName == localName)
                    return child;
            }

            return null;
        }

        public static T GetFirstDescendantValue<T>(this XElement element, string localName)
        {
            var descendant = GetFirstDescendant(element, localName);
            if (descendant == null || string.IsNullOrEmpty(descendant.Value))
                return default(T);

            return (T)Convert.ChangeType(descendant.Value, typeof(T));
        }
    }
}
