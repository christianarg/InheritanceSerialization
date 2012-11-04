using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public class InheritanceSerializerNodeDiscriminator
    {
        private const string DISCRIMINATOR = "discriminator";

        public static string Serialize(object info)
        {
            var serializer = new XmlSerializer(info.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, info);
                stream.Position = 0;
            
                var xmlDocument = new XmlDocument();

                xmlDocument.Load(stream);
                var node = xmlDocument.CreateNode(XmlNodeType.Element, DISCRIMINATOR, "");
                node.InnerText = GetTypeFullName_With_AssemblyName_WihoutVersion(info.GetType());
                xmlDocument.DocumentElement.AppendChild(node);
                return xmlDocument.OuterXml;
            }
        }

        public static object Deserialize(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            var discriminator = xmlDocument.DocumentElement.LastChild;
            var typeName = discriminator.InnerText;

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
