using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{

    [ZenDeskSerialization(Name = "entry")]
    public class Entry
    {
        [ZenDeskSerialization(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "forum-id")]
        public int ForumId { get; set; }

        [ZenDeskSerialization(Name = "title")]
        public string Title { get; set; }

        [ZenDeskSerialization(Name = "body")]
        public string Body { get; set; }

        [ZenDeskSerialization(Name = "created-at")]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "updated-at")]
        public string UpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "hits")]
        public int Hits { get; set; }

        [ZenDeskSerialization(Name = "posts-count")]
        public int PostsCount { get; set; }

        [ZenDeskSerialization(Name = "is-locked")]
        public bool IsLocked { get; set; }

        [ZenDeskSerialization(Name = "is-pinned")]
        public bool IsPinned { get; set; }

        [ZenDeskSerialization(Name = "is-public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerialization(Name = "submitter-id")]
        public int SubmitterId { get; set; }

        [ZenDeskSerialization(Name = "posts")]
        public List<Post> Posts { get; set; }

        /// <summary>
        /// Used only on Post. Api does not return this on Get
        /// </summary>
        [ZenDeskSerialization(Name = "current-tags")]
        public string CurrentTags { get; set; }
    }
}
