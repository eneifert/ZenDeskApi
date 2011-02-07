using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi
{
    public class ZenRestRequest : RestRequest
    {
        public ZenRestRequest()
        {
            RequestFormat = DataFormat.Xml;
            AddHeader("Content-Type", "application/xml");
            XmlSerializer = new ZenDeskXmlSerializer();
        }
    }
}
