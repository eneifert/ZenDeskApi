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

        private const string Entries = "entries";
        private const string Posts = "posts";

        public List<Entry> GetEntriesByForumId(int forumId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("forums/{0}/{1}.json", forumId, Entries)
            };

            return Execute<List<Entry>>(request);
        }

        public Entry GetEntryById(int id)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.json", Entries, id)
            };

            return Execute<Entry>(request);
        }      

        public int CreateEntry(int forumId, int submitterId, string title, string body, string currentTags, bool isLocked=false, bool isPinned=true)
        {
            return
                CreateEntry(new Entry
                                {
                                    SubmitterId = submitterId,
                                    ForumId = forumId,
                                    Title = title,
                                    Body = body,
                                    CurrentTags = currentTags,
                                    IsLocked = isLocked,
                                    IsPinned = isPinned
                                });
        }

        public int CreateEntry(Entry entry)
        {
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = Entries + ".xml"
            };

            request.AddBody(entry);

            var res = Execute(request);

            return GetIdFromLocationHeader(res);
        }

        public bool UpdateEntry(Entry entry)
        {
            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", Entries, entry.Id)
            };

            request.AddBody(entry);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyEntry(int entryId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", Entries, entryId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public Post GetPostByIds(int entityId, int postId)
        {
            var entity = GetEntryById(entityId);

            return entity.Posts.FirstOrDefault(x => x.Id == postId);
        }
        

        public int CreatePost(int entryId, int userId, string body)
        {
            return CreatePost(new Post { EntryId = entryId, Body = body, UserId = userId });
        }
      
        public int CreatePost(Post post)
        {
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = Posts
            };

            request.AddBody(post);

            var res = Execute(request);

            return GetIdFromLocationHeader(res);
        }

        public bool UpdatePost(Post post)
        {
            if (post.Id <= 0)
                return false;

            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", Posts, post.Id)
            };

            request.AddBody(post);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyPost(int postId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", Posts, postId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
