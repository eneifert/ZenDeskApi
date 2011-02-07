using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{

    [ZenDeskSerializeAs(Name = "forum")]
    public class Forum
    {
        [ZenDeskSerializeAs(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerializeAs(Name = "description")]
        public string Description { get; set; }

        [ZenDeskSerializeAs(Name = "entries-count")]
        public int EntriesCount { get; set; }

        [ZenDeskSerializeAs(Name = "posts-count", Skip=true)]
        public int PostsCount { get; set; }

        [ZenDeskSerializeAs(Name = "is-locked")]
        public bool IsLocked { get; set; }

        [ZenDeskSerializeAs(Name = "is-public", Skip=true)]
        public bool IsPublic { get; set; }
    }
}
