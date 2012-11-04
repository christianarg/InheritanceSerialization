using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace InheritanceSerialization
{
    public class AttributeDiscriminatorPolicy : IDiscriminatorPolicy
    {
        public void CreateDiscriminator(object info, XmlDocument xmlDocument)
        {
            var node = xmlDocument.CreateAttribute("", Constants.DISCRIMINATOR, "");
            node.InnerText = ReflectionUtils.GetTypeFullName_With_AssemblyName_WihoutVersion(info.GetType());
            xmlDocument.DocumentElement.Attributes.Append(node);
        }

        public Type RetrieveRealType(XmlDocument xmlDocument)
        {
            // read "discriminator"
            var discriminator = xmlDocument.DocumentElement.Attributes[Constants.DISCRIMINATOR];
            var typeName = discriminator.InnerText;
            return Type.GetType(typeName);
        }
    }
}
