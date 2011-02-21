using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "organization")]
    public class Organization
    {

        [ZenDeskSerialization(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerialization(Name = "is-shared")]
        public bool IsShared { get; set; }

        [ZenDeskSerialization(Name = "default")]
        public string Default { get; set; }

        [ZenDeskSerialization(Name = "users")]
        public List<User> Users { get; set; }
    }
}
