using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RestSharp.Extensions;

namespace ZenDeskApi.XmlSerializers
{
    public static class ZenDeskXmlExtensions
    {
        /// <summary>
        /// Returns the name of an element with the namespace if specified
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="namespace">XML Namespace</param>
        /// <returns></returns>
        public static XName AsNamespaced(this string name, string @namespace)
        {
            XName xName = name;

            if (@namespace.HasValue())
            {
                var xn = XName.Get(name, @namespace);
                if (xn != null)
                    xName = xn;
            }
            return xName;                      
        }      
    }
}
