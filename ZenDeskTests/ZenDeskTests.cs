using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ZenDeskApi;
using ZenDeskApi.Model;

namespace ZenDeskTests
{
    [TestFixture]
    public class ZenDeskTests
    {
        ZenDeskApi.ZenDeskApi _api = new ZenDeskApi.ZenDeskApi(ZenDeskSettings.Site, ZenDeskSettings.Email,
                                                                   ZenDeskSettings.Password);
        #region Tests that modify an account are commented out

        [Test]
        public void CanCreateUpdateAndDestroyEntities()
        {
            int forumId = _api.GetForums().First().Id;

            int id = _api.CreateEntry(forumId, (int)_api.QueryUsers(ZenDeskSettings.Email).First().Id, "test entry", "entry body", "some tags");

            Assert.Greater(id, 0);

            var entry = _api.GetEntryById(id);
            entry.Title = "new title";

            Assert.True(_api.UpdateEntry(entry));

            var entry2 = _api.GetEntryById(id);
            Assert.AreEqual(entry2.Title, entry.Title);

            Assert.True(_api.DestroyEntry(id));
        }

        [Test]
        public void CanCreateUpdateAndDestroyPosts()
        {
            int forumId = _api.GetForums().First().Id;
            int entryId = _api.GetEntriesByForumId(forumId).First().Id;
            int userId = (int)_api.FindUsersByEmail(ZenDeskSettings.Email).First().Id;

            int id = _api.CreatePost(entryId, userId, "some random post");

            Assert.Greater(id, 0);

            //Update the post
            var post = _api.GetPostByIds(entryId, id);
            post.Body = "new body";
            Assert.True(_api.UpdatePost(post));

            var post2 = _api.GetPostByIds(entryId, id);
            Assert.AreEqual(post.Body, post2.Body);

            Assert.True(_api.DestroyPost(id));
        }

        [Test]
        public void CanCreateUpdateAndDestroyForum()
        {
            string name = "Test Forum";
            string desc = "forum test";

            int id = _api.CreateOrUpdateForum(name, desc, false);

            Assert.Greater(id, 0);

            var forum = _api.GetForumById(id);
            forum.Name = "Changed Test Forum";

            Assert.Greater(_api.CreateOrUpdateForum(forum), 0);

            Assert.AreEqual(_api.GetForumById(id).Name, forum.Name);

            Assert.True(_api.DestroyForum(id));
        }

        [Test]
        public void CanApplyMacro()
        {
            int macroId = _api.GetMacros().Last().Id;
            int ticketId = _api.GetTicketsForUserByPage(ZenDeskSettings.EndUserEmail).FirstOrDefault().NiceId;

            Assert.True(_api.ApplyMacro(macroId, ticketId));
        }

        [Test]
        public void CanCreateTicketWithRequester()
        {
            Assert.Greater(_api.CreateTicketWithRequester("test", TicketPriorities.Normal, "a name", ZenDeskSettings.EndUserEmail), 0);
        }

        [Test]
        public void CanCreateUpdateAndDestroyTicket()
        {

            int requesterId = (int)_api.FindUsersByEmail(ZenDeskSettings.EndUserEmail).First().Id;
            int id = _api.CreateTicket("test", requesterId, TicketPriorities.Normal, "test");
            Assert.Greater(id, 0);

            ////It's not really clear how to test anyone else's ticket fields but mine so this is commented out.
            //var t1 = _api.GetTicketById(id);
            //t1.TicketFieldEntries.Add(new TicketFieldEntry { TicketFieldId = ZenDeskSettings.CustomFieldId, Value = "4323234" });

            //Assert.True(_api.UpdateTicket(t1));

            Assert.True(_api.AddComment(id, new Comment { Value = "updated comment value" }));
            var t2 = _api.GetTicketById(id);

            Assert.AreEqual(t2.Comments.Last().Value, "updated comment value");

            Assert.True(_api.DestroyTicket(id));
        }

        [Test]
        public void CanCreateATicketWithMoreFields()
        {
            var ticketFieldEntries = new List<TicketFieldEntry>();
            ticketFieldEntries.Add(new TicketFieldEntry { TicketFieldId = ZenDeskSettings.CustomFieldId, Value = "555-555-5555" });

            //Create a new SR Ticket using ZenDesk
            var ticket = new ZenDeskApi.Model.Ticket
            {
                RequesterEmail = "somenewemail@sample.com",
                TicketFieldEntries = ticketFieldEntries,
                Description = "Hello world \n this is a ticket!",
                RequesterName = string.Format("{0} {1}", "John", "Smith"),
                SetTags = "Colorodo",
                Subject = "Promotion",
                TicketTypeId = (int)TicketType.Question,
            };
            int resId = _api.CreateTicket(ticket);

            Assert.Greater(resId, 0);
        }

        [Test]
        public void CanCreateUpdateAndDestroyTicketAsEndUser()
        {
            int id = _api.CreateTicketAsEndUser(ZenDeskSettings.EndUserEmail, "I am testing", "still testing");

            Assert.Greater(id, 0);

            string desc = "updated comment value";
            Assert.True(_api.UpdateTicketAsEndUser(id, desc));
            var tick = _api.GetTicketById(id);

            Assert.AreEqual(tick.Comments.Last().Value, desc);

            Assert.True(_api.DestroyRequest(id));
        }

        [Test]
        public void CanCreateUpdateAndDestroyGroup()
        {
            string name = "Test Api Group";
            var gs = _api.GetGroups().First(x => x.Name == "Support");

            int id = _api.CreateOrUpdateGroup(name, gs.Users.Select(x => (int)x.Id).ToList());
            Assert.Greater(id, 0);

            Group g = _api.GetGroupById(id);
            g.Name = "Updated Test Api Group";
            Assert.True(_api.UpdateGroup(g));

            Group g2 = _api.GetGroupById(id);
            Assert.AreEqual(g2.Name, g.Name);

            Assert.AreEqual(g2.Users.Count, gs.Users.Count);
            Assert.True(_api.DestroyGroup(g.Id));
        }

        [Test]
        public void CanCreateUpdateAndDestroyOrganization()
        {
            string name = "Api Org";
            string site = "www.google.com";

            Organization o = new Organization() { Name = name, Default = site };
            int id = _api.CreateOrUpdateOrganization(o);

            Assert.Greater(id, 0);

            var org = _api.GetOgranizationById(id);
            org.Default = "www.yahoo.com";
            _api.CreateOrUpdateOrganization(org);

            Assert.AreEqual(_api.GetOgranizationById(id).Default, org.Default);

            Assert.True(_api.DestroyOrganization(id));
        }

        [Test]
        public void CanCreateUpdateAndDestroyAUser()
        {
            string email = "tsdestestss@test.com";

            int userID = _api.CreateOrUpdateUser(email, "some random name", "password", Role.EndUser,
                                                 RestrictedTo.TicketsRequestedByUser, _api.GetAllGroupIDs());
            Assert.Greater(userID, 0);

            var u = _api.GetUserById(userID);
            u.Phone = "342098329342908";

            Assert.True(_api.UpdateUser(u));
            var u2 = _api.GetUserById(userID);
            Assert.AreEqual(u2.Phone, u.Phone);

            Assert.True(_api.DestroyUser(userID));
        }

        [Test]
        public void CanAddEmailToUser()
        {
            Assert.True(_api.AddEmailAddressToAUser((int)_api.QueryUsers(ZenDeskSettings.EndUserEmail).First().Id,
                                                    "fromapi@test.com"));
        }

        [Test]
        public void CanAddTwitterHandleToUser()
        {

            string twit = "ss31232089_sample_twitter_handle";
            int userId = (int)_api.QueryUsers(ZenDeskSettings.Email).First().Id;

            try
            {
                _api.AddTwitterHandleToUser(userId, twit);
            }
            catch (ZenDeskNotAcceptableInputException e)
            {
                Assert.AreEqual(e.Message, string.Format("Twitter handle '{0}' does not exist.", twit));
            }

            Assert.True(_api.AddTwitterHandleToUser(userId, ZenDeskSettings.TwitterName));

        }
        #endregion
  
        [Test]
        public void CanGetUsers()
        {          
            List<User> users = _api.GetUsers();
            
            Assert.Greater(users.Count, 0);
        }

        [Test]
        public void CanGetUserById()
        {
            int id = (int)_api.GetUsers().First().Id;

            Assert.AreEqual(_api.GetUserById(id).Id , id);
        }

        [Test]
        public void CanQueryUsers()
        {
            Assert.AreEqual(_api.QueryUsers(ZenDeskSettings.Email).Count, 1);
        }
 
        
        [Test]
        public void CanGetUserIdentities()
        {
            User u = _api.QueryUsers(ZenDeskSettings.Email).First();
            var idents = _api.GetUserIdentities((int) u.Id);

            Assert.Greater(idents.Count, 0);
        }
     
        [Test]
        public void CanGetOrganizations()
        {
            var orgs = _api.GetOgranizations();
            Assert.Greater(orgs.Count, 0);

            var org = _api.GetOgranizationById(orgs.First().Id);
            Assert.Greater(org.Id, 0);
        }
        
        [Test]
        public void CanGetGroups()
        {
            Assert.Greater(_api.GetGroups().Count, 0);
        }


        [Test]
        public void CanGetATicket()
        {            
            Ticket t = _api.GetTicketById(1);

            Assert.Greater(t.Comments.Count, 0);
        }

        //Needs an email that created their own tickets
        [Test]
        public void CanGetTicketsForUser()
        {
            List<Ticket> ts = _api.GetAllTicketsForUser(ZenDeskSettings.EndUserEmail);

            Assert.Greater(ts.Count, 0);
        }

        [Test]
        public void CanGetAllTicketsInAView()
        {
            View v = _api.GetViewByName("My unsolved tickets");
            List<Ticket> ts = _api.GetAllTicketsInView(v.Id);

            Assert.Greater(ts.Count, 0);
        }

        [Test]
        public void CanGetViews()
        {
            var vs = _api.GetViews();

            Assert.Greater(vs.Count, 0);
        }        
    
        [Test]
        public void CanGetTags()
        {
            Assert.Greater(_api.GetAllTags().Count, 0);
        }

        [Test]
        public void CanGetTicketsForTag()
        {
            var tags = _api.GetAllTags();

            Assert.Greater(_api.GetAllTicketsForTag(tags.First().Name).Count, 0);
        }

        [Test]
        public void CanGetForums()
        {
            var forums = _api.GetForums();
            Assert.Greater(forums.Count, 0);

            var forum = _api.GetForumById(forums.First().Id);
            Assert.Greater(forum.Id, 0);
        }

        [Test]
        public void CanGetTicketFields()
        {
            Assert.Greater(_api.GetTicketFields().Count, 0);
        }

        [Test]
        public void CanGetMacros()
        {
            Assert.Greater(_api.GetMacros().Count, 0);
        }       
    }
}
