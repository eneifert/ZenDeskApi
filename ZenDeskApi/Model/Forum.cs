using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{

    [ZenDeskSerialization(Name = "forum")]
    public class Forum
    {
        [ZenDeskSerialization(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerialization(Name = "description")]
        public string Description { get; set; }

        [ZenDeskSerialization(Name = "entries-count")]
        public int EntriesCount { get; set; }

        [ZenDeskSerialization(Name = "posts-count", Skip=true)]
        public int PostsCount { get; set; }

        [ZenDeskSerialization(Name = "is-locked")]
        public bool IsLocked { get; set; }

        [ZenDeskSerialization(Name = "is-public", Skip=true)]
        public bool IsPublic { get; set; }
    }
}
