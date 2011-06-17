using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using ZenDeskApi.Model;

namespace ZenDeskApi
{
    public partial class ZenDeskApi 
    {
        private const string Groups = "groups";

        public List<Group> GetGroups()
        {
            return GetCollection<Group>(Groups + ".xml");
        }

        public Group GetGroupById(int id)
        {
            var groups = GetGroups();

            var g = groups.Where(x => x.Id == id);

            if (g.Count() > 0)
                return g.First();

            return null;
        }

        public int CreateOrUpdateGroup(string name, List<int> userIds)
        {
            return CreateOrUpdateGroup(new Group() { Name = name, UserIds = userIds });
        }

        /// <summary>
        /// Note: You cannot add end users to groups they go in organizations.        
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int CreateOrUpdateGroup(Group group)
        {            
            //Check to see if a group already exists
            var oldGroups = GetGroups().Where(x => x.Name == group.Name);
            if (oldGroups.Count() > 0)
            {
                group.Id = oldGroups.First().Id;
                if (UpdateGroup(group))
                    return group.Id;

                return -1;
            }

            //Make sure to use the UserIds not the Users
            group.CopyUsersToUserIds();       

            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = Groups + ".xml"
            };

            request.AddBody(group);

            var res = Execute(request);

            return GetIdFromLocationHeader(res);
          
        }

        public bool UpdateGroup(Group group)
        {
            //Make sure to use the UserIds not the Users
            group.CopyUsersToUserIds();

            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", Groups, group.Id)
            };

            request.AddBody(group);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyGroup(int groupId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", Groups, groupId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }


        public int[] GetAllGroupIDs()
        {
            var groups = GetGroups();
            var ids = new int[groups.Count];

            for(int i =0; i < groups.Count; i++)
            {
                ids[i] = groups[i].Id;
            }

            return ids;            
        }
    }
}
