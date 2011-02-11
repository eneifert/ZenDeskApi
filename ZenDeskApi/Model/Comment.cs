using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "comment")]
    public class Comment
    {
        [ZenDeskSerializeAs(Name = "author-id")]
        public int AuthorId { get; set; }

        [ZenDeskSerializeAs(Name = "created-at", Skip=true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "is-public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerializeAs(Name = "type")]
        public string CommentType { get; set; }

        [ZenDeskSerializeAs(Name = "value")]
        public string Value { get; set; }

        [ZenDeskSerializeAs(Name = "via-id")]
        public int ViaId { get; set; }

        [ZenDeskSerializeAs(Name = "attachments")]
        public List<Attachment> Attachments { get; set; }

    } 
}
