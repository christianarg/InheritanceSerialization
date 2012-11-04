using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceSerialization
{
    public static class DiscriminatotyPolicyFactory
    {
        public static IDiscriminatorPolicy GetDiscriminatorPolicy(DiscriminatorType policyType)
        {
            switch (policyType)
            {
                case DiscriminatorType.Attribute:
                    return new AttributeDiscriminatorPolicy();
                    break;
                case DiscriminatorType.Node:
                    return new NodeDiscriminatorPolicy();
                    break;
                default:
                    throw new Exception("Unable to determine the policy");
            }
        }
    }
}
