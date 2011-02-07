using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Deserializers;
using ZenDeskApi.Model;
using ZenDeskApi.XmlSerializers;


namespace ZenDeskApi
{
    public partial class ZenDeskApi
    {
        private const string Users = "users";

        public List<User> GetUsers()
        {
            return GetCollection<User>(Users); 
        }


        public User GetUserById(int id)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.json", Users, id.ToString())
            };

            return Execute<User>(request);
        }

        /// <summary>
        /// Matches the api method at http://www.zendesk.com/api/users
        /// Ex: QueryUsers("myemailaddress@msn.com") returns a list of users with that email, which should only be one.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<User> QueryUsers(string query)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = Users,
            };

            request.AddParameter("query", query);

            return Execute<List<User>>(request);
        }

        /// <summary>
        /// Same method as QueryUsers but makes it's most common use more clear.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<User> FindUsersByEmail(string email)
        {
            return QueryUsers(email);
        }

        public int CreateOrUpdateUser(string email, string name, string password, Role role, RestrictedTo restrictionId, int[] groupIDs, bool isVerified = true)
        {
            return CreateOrUpdateUser(new User
                           {
                               Email = email,
                               Name = name,
                               Password = password,
                               Role = (int) role,
                               RestrictionId = (int) restrictionId,
                               GroupIds =  groupIDs.ToList(),
                               IsVerified = isVerified                               
                           });            
        }

        /// <summary>
        /// Creates the new user and returns their id. Or updates if that email address is already in the system.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public int CreateOrUpdateUser(User newUser)
        {
            //Update if the user exists
            var users = QueryUsers(newUser.Email);
            if (users.Count > 0)
            {
                newUser.Id = users.First().Id;

                if (!UpdateUser(newUser))
                    return -1;

                return (int)users.First().Id;
            }

            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = Users + ".xml",
            };

            request.AddBody(newUser);
           
            var res = Execute(request);

            return GetIdFromLocationHeader(res);
        }

        public bool UpdateUser(User user)
        {

            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", Users, user.Id)
            };

            request.AddBody(user);

            var res = Execute(request);
     
            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }


        public bool DestroyUser(int userId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", Users, userId)
            };

            var res = Execute(request);
            
            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public List<UserIdentity> GetUserIdentities(int userId)
        {
            return GetCollection<UserIdentity>(string.Format("{0}/{1}/user_identities.json", Users, userId)); 
        }


        public bool AddEmailAddressToAUser(int userId, string email)
        {
            //first check to see if the user allready has that email identity
            var emails = GetUserIdentities(userId);
            if (emails.Where(x => x.Value == email).Count() > 0)
                return true;
            
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = string.Format("{0}/{1}/user_identities.xml", Users, userId)
            };

            request.AddParameter("text/xml", string.Format("<email>{0}</email>", email), ParameterType.RequestBody);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.Created;
        }


        /// <summary>
        /// Throws a ZenDeskNotAcceptableInputException if the twitter account doesn't exist or is already assigned to someone else.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="twitterHandle"></param>
        /// <returns></returns>
        public bool AddTwitterHandleToUser(int userId, string twitterHandle)
        {
            //first check to see if the user already has that twitter identity
            var records = GetUserIdentities(userId);
            if (records.Where(x => x.ScreenName == twitterHandle).Count() > 0)
                return true;

            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = string.Format("{0}/{1}/user_identities.xml", Users, userId)
            };

            request.AddParameter("text/xml", string.Format("<twitter>{0}</twitter>", twitterHandle), ParameterType.RequestBody);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.Created;
        }

        public bool MakeUserIdentityPrimary(int userId, int identityId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = string.Format("/{0}/{1}/user_identities/{2}/make_primary", Users, userId, identityId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }


        public bool DeleteUserIdentity(int userId, int identityId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("/{0}/{1}/user_identities/{2}", Users, userId, identityId)
            };

            var res = Execute(request);

            //If it is not found then it is already deleted
            return res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.NotFound;
        }
    }   
}
