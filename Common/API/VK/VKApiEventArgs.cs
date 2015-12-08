using System;
using System.Xml;

namespace OFClassLibrary.Common.API.VK
{
    class VKApiEventArgs:EventArgs
    {
        public readonly XmlDocument Data;

        public VKApiEventArgs() : this(null)
        {

        }

        public VKApiEventArgs(XmlDocument data)
        {
            Data = data;
        }
    }
}
