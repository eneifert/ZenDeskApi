using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "group")]
    public class Group
    {

        [ZenDeskSerializeAs(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerializeAs(Name = "id", Skip=true)]
        public int Id { get; set; }

        [ZenDeskSerializeAs(Name = "is-active", Skip = true)]
        public bool IsActive { get; set; }

        [ZenDeskSerializeAs(Name = "created-at", Skip = true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "updated-at", Skip = true)]
        public string UpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "users", Skip=true)]
        public List<User> Users { get; set; }

        [ZenDeskSerializeAs(Name = "agents", ListItemName="agent")]
        public List<int> UserIds { get; set; }

        public void CopyUsersToUserIds()
        {

            if (Users != null && Users.Count > 0)                
                UserIds = Users.Select(x => (int)x.Id).ToList();            
        }
                
    }
}
