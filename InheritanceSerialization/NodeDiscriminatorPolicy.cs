using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace InheritanceSerialization
{
    public class NodeDiscriminatorPolicy : IDiscriminatorPolicy
    {
        public void CreateDiscriminator(object info, System.Xml.XmlDocument xmlDocument)
        {
            var node = xmlDocument.CreateNode(XmlNodeType.Element, Constants.DISCRIMINATOR, "");
            node.InnerText = ReflectionUtils.GetTypeFullName_With_AssemblyName_WihoutVersion(info.GetType());
            xmlDocument.DocumentElement.AppendChild(node);
        }

        public Type RetrieveRealType(System.Xml.XmlDocument xmlDocument)
        {
            var discriminator = xmlDocument.DocumentElement.LastChild;
            var typeName = discriminator.InnerText;
            return Type.GetType(typeName);
        }
    }
}
