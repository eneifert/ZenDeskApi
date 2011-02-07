using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Contrib;


namespace ZenDeskApi
{   
    public partial class ZenDeskApi
    {
        private const string  XOnBehalfOfEmail = "X-On-Behalf-Of";
        private RestClient _client;

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="yourZenDeskUrl"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ZenDeskApi(string yourZenDeskUrl, string user, string password)
        {                     
            _client = new RestClient(yourZenDeskUrl);
            _client.Authenticator = new HttpBasicAuthenticator(user, password);            

        }
         

		public T Execute<T>(ZenRestRequest request) where T : new()
		{
			var response = _client.Execute<T>(request);		    
			return response.Data;
		}

        public RestResponse Execute(ZenRestRequest request)
		{
			var res = _client.Execute(request);
            ValidateZenDeskRestResponse(res);
            return res;
		}

        /// <summary>
        /// Gets the Collection
        /// </summary>
        /// <returns></returns>
        public List<T> GetCollection<T>(string resource, string rootElement="")
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = resource,
                RootElement = rootElement
            };

            return Execute<List<T>>(request);
        }

        protected void ValidateZenDeskRestResponse(RestResponse response)
        {
            if(response.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
            {
                string error = "ZenDesk could not handle the input you gave it";
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response.Content);
                    error = doc.DocumentElement["error"].FirstChild.Value;
                }
                catch (Exception)
                {}

                throw new ZenDeskNotAcceptableInputException(error);
            }
        }

        private int GetIdFromLocationHeader(RestResponse res)
        {
            int id = -1;

            var h = res.Headers.Where(x => x.Name == "Location");
            if(h.Count() > 0)
            {
                string[] userUrl = h.First().Value.ToString().Split('/');
                string idString = userUrl.Last().Replace(".xml", "").Split('-').First();
                                
                int.TryParse(idString, out id);                
            }

            return id;
        }
    }

    public class ZenDeskNotAcceptableInputException : Exception
    {
        public ZenDeskNotAcceptableInputException(string message) : base(message)
        { }
    }
}
