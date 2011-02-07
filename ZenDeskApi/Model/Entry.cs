using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{

    [ZenDeskSerializeAs(Name = "entry")]
    public class Entry
    {
        [ZenDeskSerializeAs(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "forum_id")]
        public int ForumId { get; set; }

        [ZenDeskSerializeAs(Name = "title")]
        public string Title { get; set; }

        [ZenDeskSerializeAs(Name = "body")]
        public string Body { get; set; }

        [ZenDeskSerializeAs(Name = "created_at")]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "hits")]
        public int Hits { get; set; }

        [ZenDeskSerializeAs(Name = "posts_count")]
        public int PostsCount { get; set; }

        [ZenDeskSerializeAs(Name = "is_locked")]
        public bool IsLocked { get; set; }

        [ZenDeskSerializeAs(Name = "is_pinned")]
        public bool IsPinned { get; set; }

        [ZenDeskSerializeAs(Name = "is_public")]
        public bool IsPublic { get; set; }

        [ZenDeskSerializeAs(Name = "submitter_id")]
        public int SubmitterId { get; set; }

        [ZenDeskSerializeAs(Name = "posts")]
        public List<Post> Posts { get; set; }

        /// <summary>
        /// Used only on Post. Api does not return this on Get
        /// </summary>
        [ZenDeskSerializeAs(Name = "current-tags")]
        public string CurrentTags { get; set; }
    }
}
