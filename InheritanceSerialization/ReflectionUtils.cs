using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceSerialization
{
    public static class ReflectionUtils
    {
        public static string GetTypeFullName_With_AssemblyName_WihoutVersion(Type type)
        {
            return string.Format("{0},{1}", type.FullName, type.Assembly.GetName().Name);
        }
    }
}
