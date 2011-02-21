using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "post")]
    public class Post
    {
        [ZenDeskSerialization(Name = "user-id")]
        public int UserId { get; set; }
        
        [ZenDeskSerialization(Name = "created-at", Skip=true)]
        public string CreatedAt { get; set; }
        
        [ZenDeskSerialization(Name = "updated-at", Skip=true)]
        public string UpdatedAt { get; set; }
        
        [ZenDeskSerialization(Name = "body")]
        public string Body { get; set; }

        [ZenDeskSerialization(Name = "entry-id")]
        public int EntryId { get; set; }

        [ZenDeskSerialization(Name = "forum-id")]
        public int ForumId { get; set; }

        [ZenDeskSerialization(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "is-informative")]
        public bool IsInformative { get; set; }

        [ZenDeskSerialization(Name = "user", Skip=true)]
        public User User { get; set; }
    }
}
