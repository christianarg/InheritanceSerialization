using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace InheritanceSerialization
{

    /// <summary>
    /// Allows to serialize / deserialize  objetcs and inheritance hierarchies
    /// WITHOUT knowing the type to serialize
    /// 
    /// When serializing adds a "discriminator" based on the real type
    ///
    /// When deserializing "infers" the real type based on the discriminator saved during the serialization
    /// </summary>
    public class InheritanceSerializer
    {
        private const string DISCRIMINATOR = "discriminator";

        public static string Serialize(object info)
        {
            var serializer = new XmlSerializer(info.GetType());
            using (var stream = new MemoryStream())
            {
                // Serialize
                serializer.Serialize(stream, info);
                stream.Position = 0;

                // Open serialization output
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);
                // Add a "discriminador" based on the real type, to use it during deserialization
                var node = xmlDocument.CreateAttribute("", DISCRIMINATOR, "");
                node.InnerText = GetTypeFullName_With_AssemblyName_WihoutVersion(info.GetType());
                xmlDocument.DocumentElement.Attributes.Append(node);

                // return the xml with the discriminator
                return xmlDocument.OuterXml;
            }
        }

        public static object Deserialize(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            // read "discriminator"
            var discriminator = xmlDocument.DocumentElement.Attributes[DISCRIMINATOR];
            var typeName = discriminator.InnerText;

            // now we know the real type based on the discriminator to deserialize 
            var serializer = new XmlSerializer(Type.GetType(typeName));

            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                return serializer.Deserialize(stream);
            }
        }

        public static string GetTypeFullName_With_AssemblyName_WihoutVersion(Type type)
        {
            return string.Format("{0},{1}", type.FullName, type.Assembly.GetName().Name);
        }
    }
}
