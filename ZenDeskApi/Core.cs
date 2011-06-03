using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using RestSharp;
using RestSharp.Contrib;
using ZenDeskApi.Model;
using ZenDeskApi.XmlSerializers;


namespace ZenDeskApi
{
    public partial class ZenDeskApi
    {
        private const string  XOnBehalfOfEmail = "X-On-Behalf-Of";
        private RestClient _client;
        private string _user;
        private string _password;
        private string _zenDeskUrl;

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="yourZenDeskUrl"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ZenDeskApi(string yourZenDeskUrl, string user, string password)
        {
            _user = user;
            _password = password;
            _zenDeskUrl = yourZenDeskUrl;

            _client = new RestClient(yourZenDeskUrl);
            _client.Authenticator = new HttpBasicAuthenticator(user, password);
            _client.AddHandler("application/xml; charset=utf-8", new ZenDeskXmlDeserializer());
            _client.AddHandler("application/xml", new ZenDeskXmlDeserializer());                        
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
        /// When using sso use this method to generate a url that logs a user in and returns them to the returnToUrl (if specified).
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="httpsUrl"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="returnToUrl"></param>
        /// <returns></returns>
        public static string GetLoginUrl(string authToken, string httpsUrl, string name, string email, string returnToUrl = "")
        {
            string url = string.Format("{0}/access/remote/", httpsUrl);

            string timestamp = GetUnixEpoch(DateTime.Now.AddDays(1)).ToString();

            string message = name + email + authToken + timestamp;
            string hash = Md5(message);

            string result = url + "?name=" + HttpUtility.UrlEncode(name) +
                   "&email=" + HttpUtility.UrlEncode(email) +
                   "&timestamp=" + timestamp +
                   "&hash=" + hash;

            if (returnToUrl.Length > 0)
                result += "&return_to=" + returnToUrl;

            return result;
        }

        static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }


        static string Md5(string strChange)
        {
            //Change the syllable into UTF8 code
            byte[] pass = Encoding.UTF8.GetBytes(strChange);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(pass);
            string strPassword = ByteArrayToHexString(md5.Hash);
            return strPassword;
        }

        static string ByteArrayToHexString(byte[] Bytes)
        {
            // important bit, you have to change the byte array to hex string or zenddesk will reject
            StringBuilder Result;
            string HexAlphabet = "0123456789abcdef";

            Result = new StringBuilder();

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
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

        string RemoveAllXmlAttributes(string xml)
        {


            XmlDocument d = new XmlDocument();
            d.LoadXml(xml);
            d.DocumentElement.Attributes.RemoveAll();
            ClearAllAttributes(d.DocumentElement.ChildNodes);

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            d.WriteTo(tx);

            return sw.ToString();
        }
        void ClearAllAttributes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes != null)
                    node.Attributes.RemoveAll();

                if (node.HasChildNodes)
                    ClearAllAttributes(node.ChildNodes);
            }
        }
    }

    public class ZenDeskNotAcceptableInputException : Exception
    {
        public ZenDeskNotAcceptableInputException(string message) : base(message)
        { }
    }
}
