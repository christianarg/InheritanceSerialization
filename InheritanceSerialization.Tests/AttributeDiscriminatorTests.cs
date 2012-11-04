using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace InheritanceSerialization.Tests
{
    [TestClass]
    public class AttributeDiscriminatorTests
    {

        const string resourceInfoXml = @"<?xml version=""1.0""?><ResourceInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" discriminator=""InheritanceSerialization.Tests.ResourceInfo,InheritanceSerialization.Tests""><ResourceProperty>Hola</ResourceProperty></ResourceInfo>";
        const string videoResourceXml = @"<?xml version=""1.0""?><VideoInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" discriminator=""InheritanceSerialization.Tests.VideoInfo,InheritanceSerialization.Tests""><ResourceProperty>Maestro</ResourceProperty><VideoProperty>Soy Video</VideoProperty></VideoInfo>";

        [TestMethod]
        public void AttributeDiscriminator_SerializeSimple()
        {
            var r = new ResourceInfo() { ResourceProperty = "Hola" };

            var xml = InheritanceSerializer.Serialize(r);

            Assert.AreEqual(resourceInfoXml, xml);
        }

        [TestMethod]
        public void AttributeDiscriminator_DeserializeSimple()
        {
            var resourceInfo = InheritanceSerializer.Deserialize(resourceInfoXml) as ResourceInfo;
            Assert.AreEqual("Hola", resourceInfo.ResourceProperty);
        }


        [TestMethod]
        public void AttributeDiscriminator_SerializeVideo()
        {
            var r = new VideoInfo() { ResourceProperty = "Maestro", VideoProperty = "Soy Video" };

            var xml = InheritanceSerializer.Serialize(r);

            Assert.AreEqual(videoResourceXml, xml);
        }

        [TestMethod]
        public void AttributeDiscriminator_DeserializeVideo()
        {
            var deserialized = InheritanceSerializer.Deserialize(videoResourceXml);
            var resourceInfo = deserialized as ResourceInfo;
            var videoInfo = deserialized as VideoInfo;

            Assert.AreEqual("Maestro", resourceInfo.ResourceProperty);

            Assert.AreEqual("Maestro", videoInfo.ResourceProperty);
            Assert.AreEqual("Soy Video", videoInfo.VideoProperty);
        }
    }
}
