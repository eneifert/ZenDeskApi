using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "attachment")]
    public class Attachment
    {
        [ZenDeskSerialization(Name = "content-type")]
        public string ContentType { get; set; }

        [ZenDeskSerialization(Name = "created-at", Skip=true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "filename")]
        public string FileName { get; set; }

        [ZenDeskSerialization(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "is-public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerialization(Name = "size")]
        public int Size { get; set; }

        [ZenDeskSerialization(Name = "token")]
        public string Token { get; set; }

        [ZenDeskSerialization(Name = "url")]
        public string Url { get; set; }
    }

    public class ZenFile
    {
        public string FileName {get; set;}
        public string ContentType {get; set;}
        public byte[] FileData { get; set; }
    }
}
