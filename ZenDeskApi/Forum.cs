using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using ZenDeskApi.Model;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi
{
    public partial class ZenDeskApi 
    {
        private string _forums = "forums";

        public List<Forum> GetForums()
        {
            return GetCollection<Forum>(_forums);
        }

        public Forum GetForumById(int id)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.xml", _forums, id)
            };

            return Execute<Forum>(request);
        }

        public int CreateOrUpdateForum(string name, string description, bool isLocked)
        {
            return CreateOrUpdateForum(new Forum
                                           {
                                               Name = name,
                                               Description = description,
                                               IsLocked = isLocked
                                           });
        }

        public int GetExistingForumId(Forum forum)
        {
            //If it has an id use that
            if (forum.Id > 0)
                return forum.Id;
            
            //Otherwise get the first forum with that name
            var forums = GetForums();
            var curForum = forums.Where(x => x.Name == forum.Name);

            if (curForum.Count() > 0)
            {
                return curForum.First().Id;
            }

            //Didn't find anything
            return -1;
        }

        public int CreateOrUpdateForum(Forum forum)
        {
            int existingId = GetExistingForumId(forum);

            if(existingId > 0)
            {
                forum.Id = existingId;

                //If it couldn't be updated
                if (!UpdateForum(forum))
                    return -1;

                return forum.Id;
            }
        

            var request = new ZenRestRequest
                              {
                                  Method = Method.POST,
                                  Resource = _forums + ".xml"
                              };
            request.AddBody(forum);

            var res = Execute(request);

            return GetIdFromLocationHeader(res);
        }

        public bool UpdateForum(Forum forum)
        {
            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", _forums, forum.Id)
            };

            request.AddBody(forum);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyForum(int forumId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", _forums, forumId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
