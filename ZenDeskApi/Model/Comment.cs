using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "comment")]
    public class Comment
    {
        [ZenDeskSerialization(Name = "author-id")]
        public int AuthorId { get; set; }

        [ZenDeskSerialization(Name = "created-at", Skip=true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "is-public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerialization(Name = "type")]
        public string CommentType { get; set; }

        [ZenDeskSerialization(Name = "value")]
        public string Value { get; set; }

        [ZenDeskSerialization(Name = "via-id")]
        public int ViaId { get; set; }

        [ZenDeskSerialization(Name = "attachments")]
        public List<Attachment> Attachments { get; set; }

    } 
}
