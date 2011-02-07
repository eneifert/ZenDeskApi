using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "attachment")]
    public class Attachment
    {
        [ZenDeskSerializeAs(Name = "content-type")]
        public string ContentType { get; set; }

        [ZenDeskSerializeAs(Name = "created-at")]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "filename")]
        public string FileName { get; set; }

        [ZenDeskSerializeAs(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "is-public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerializeAs(Name = "size")]
        public int Size { get; set; }

        [ZenDeskSerializeAs(Name = "token")]
        public string Token { get; set; }

        [ZenDeskSerializeAs(Name = "url")]
        public string Url { get; set; }
    }
}
