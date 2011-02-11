using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "post")]
    public class Post
    {
        [ZenDeskSerializeAs(Name = "user-id")]
        public int UserId { get; set; }
        
        [ZenDeskSerializeAs(Name = "created-at", Skip=true)]
        public string CreatedAt { get; set; }
        
        [ZenDeskSerializeAs(Name = "updated-at", Skip=true)]
        public string UpdatedAt { get; set; }
        
        [ZenDeskSerializeAs(Name = "body")]
        public string Body { get; set; }

        [ZenDeskSerializeAs(Name = "entry-id")]
        public int EntryId { get; set; }

        [ZenDeskSerializeAs(Name = "forum-id")]
        public int ForumId { get; set; }

        [ZenDeskSerializeAs(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "is-informative")]
        public bool IsInformative { get; set; }

        [ZenDeskSerializeAs(Name = "user", Skip=true)]
        public User User { get; set; }
    }
}
