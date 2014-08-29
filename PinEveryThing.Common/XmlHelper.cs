using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PinEverything.Common
{
    public class XmlHelper
    {
        /// <summary>
        /// Deserializes an object (type indicated by <typeparamref name="T"/>) from the specified <paramref name="xml"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="xml">The xml string.</param>
        /// <returns>The object deserialized.</returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            return XmlToObject(typeof(T), xml) as T;
        }

        /// <summary>
        /// Deserializes an object (type indicated by <paramref name="type"/>) from the specified <paramref name="xml"/>.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <param name="xml">The xml string.</param>
        /// <returns>The object deserialized.</returns>
        public static object XmlToObject(Type type, string xml)
        {
            object result = null;
            using (TextReader reader = new StringReader(xml))
            {
                XmlSerializer s = new XmlSerializer(type);
                result = s.Deserialize(reader);
            }
            return result;
        }
    }
}
