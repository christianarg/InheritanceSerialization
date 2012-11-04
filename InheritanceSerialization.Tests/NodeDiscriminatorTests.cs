using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InheritanceSerialization.Tests
{

    [TestClass]
    public class NodeDiscriminatorTests
    {

        const string resourceInfoXml = @"<?xml version=""1.0""?><ResourceInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><ResourceProperty>Hola</ResourceProperty><discriminator>InheritanceSerialization.Tests.ResourceInfo,InheritanceSerialization.Tests</discriminator></ResourceInfo>";
        const string videoResourceXml = @"<?xml version=""1.0""?><VideoInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><ResourceProperty>Maestro</ResourceProperty><VideoProperty>Soy Video</VideoProperty><discriminator>InheritanceSerialization.Tests.VideoInfo,InheritanceSerialization.Tests</discriminator></VideoInfo>";
        
        [TestMethod]
        public void NodeDiscriminator_SerializeSimple()
        {
            var r = new ResourceInfo() { ResourceProperty = "Hola" };

            var xml = InheritanceSerializerNodeDiscriminator.Serialize(r);

            Assert.AreEqual(resourceInfoXml, xml);
        }

        [TestMethod]
        public void NodeDiscriminator_DeserializeSimple()
        {
            var resourceInfo = InheritanceSerializerNodeDiscriminator.Deserialize(resourceInfoXml) as ResourceInfo;
            Assert.AreEqual("Hola", resourceInfo.ResourceProperty);
        }


        [TestMethod]
        public void NodeDiscriminator_SerializeVideo()
        {
            var r = new VideoInfo() { ResourceProperty = "Maestro", VideoProperty = "Soy Video" };

            var xml = InheritanceSerializerNodeDiscriminator.Serialize(r);

            Assert.AreEqual(videoResourceXml, xml);
        }

        [TestMethod]
        public void NodeDiscriminator_DeserializeVideo()
        {
            var deserialized = InheritanceSerializerNodeDiscriminator.Deserialize(videoResourceXml);
            var resourceInfo = deserialized as ResourceInfo;
            var videoInfo = deserialized as VideoInfo;

            Assert.AreEqual("Maestro", resourceInfo.ResourceProperty);

            Assert.AreEqual("Maestro", videoInfo.ResourceProperty);
            Assert.AreEqual("Soy Video", videoInfo.VideoProperty);
        }
    }
}
