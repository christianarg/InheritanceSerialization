using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceSerialization.Tests
{
    public class ResourceInfo
    {
        public string ResourceProperty { get; set; }
    }

    public class VideoInfo : ResourceInfo
    {
        public string VideoProperty { get; set; }
    }
}
