using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InheritanceSerialization.Tests
{
    [TestClass]
    public class DiscriminatorFactoryTests
    {
        [TestMethod]
        public void Attribute_DiscriminatorType_Returns_AttributePolicy()
        {
            var policy = DiscriminatotyPolicyFactory.GetDiscriminatorPolicy(DiscriminatorType.Attribute);
            Assert.AreEqual(typeof(AttributeDiscriminatorPolicy), policy.GetType());
        }


        [TestMethod]
        public void Node_DiscriminatorType_Returns_NodePolicy()
        {
            var policy = DiscriminatotyPolicyFactory.GetDiscriminatorPolicy(DiscriminatorType.Node);
            Assert.AreEqual(typeof(NodeDiscriminatorPolicy), policy.GetType());
        }
    }
}
