using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace InheritanceSerialization
{
    public interface IDiscriminatorPolicy
    {
        void CreateDiscriminator(object info, XmlDocument xmlDocument);
        Type RetrieveRealType(XmlDocument xmlDocument);
    }
}
