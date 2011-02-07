using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "organization")]
    public class Organization
    {

        [ZenDeskSerializeAs(Name = "id")]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerializeAs(Name = "is-shared")]
        public bool IsShared { get; set; }

        [ZenDeskSerializeAs(Name = "default")]
        public string Default { get; set; }

        [ZenDeskSerializeAs(Name = "users")]
        public List<User> Users { get; set; }
    }
}
