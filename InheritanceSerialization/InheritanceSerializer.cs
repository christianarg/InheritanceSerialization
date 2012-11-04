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

        public static string Serialize(object info, DiscriminatorType discriminatorType = DiscriminatorType.Attribute)
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
                var discriminatorPolicy = DiscriminatotyPolicyFactory.GetDiscriminatorPolicy(discriminatorType);
                discriminatorPolicy.CreateDiscriminator(info, xmlDocument);

                // return the xml with the discriminator
                return xmlDocument.OuterXml;
            }
        }

        public static object Deserialize(string xml, DiscriminatorType discriminatorType = DiscriminatorType.Attribute)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            // read "discriminator" and infer the real type
            var discriminatorPolicy = DiscriminatotyPolicyFactory.GetDiscriminatorPolicy(discriminatorType);
                
            // now we know the real type based on the discriminator to deserialize 
            var serializer = new XmlSerializer(discriminatorPolicy.RetrieveRealType(xmlDocument));

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
