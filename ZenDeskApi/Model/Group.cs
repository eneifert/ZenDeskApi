using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "group")]
    public class Group
    {

        [ZenDeskSerialization(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerialization(Name = "id", Skip=true)]
        public int Id { get; set; }

        [ZenDeskSerialization(Name = "is-active", Skip = true)]
        public bool IsActive { get; set; }

        [ZenDeskSerialization(Name = "created-at", Skip = true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "updated-at", Skip = true)]
        public string UpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "users", Skip=true)]
        public List<User> Users { get; set; }

        [ZenDeskSerialization(Name = "agents", ListItemName="agent")]
        public List<int> UserIds { get; set; }

        public void CopyUsersToUserIds()
        {

            if (Users != null && Users.Count > 0)                
                UserIds = Users.Select(x => (int)x.Id).ToList();            
        }
                
    }
}
