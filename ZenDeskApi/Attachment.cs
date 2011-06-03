using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using ZenDeskApi.Model;

namespace ZenDeskApi
{
    public partial class ZenDeskApi
    {               
        public string UploadAttachment(ZenFile file)
        {
            return UploadAttachment(file, "");
        }

        public string UploadAttachments(List<ZenFile> files)
        {
            if (files.Count < 1)
                return string.Empty;

            string token = UploadAttachment(files[0]);

            if (files.Count > 1)
            {
                var otherFiles = files.Skip(1);
                foreach (var curFile in otherFiles)
                    UploadAttachment(curFile, token);
            }

            return token;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <returns></returns>       
        string UploadAttachment(ZenFile file, string token="")
        {
            var requestUrl = _zenDeskUrl;
            if (!requestUrl.EndsWith("/"))
                requestUrl += "/";

            requestUrl += string.Format("uploads.xml?filename={0}", file.FileName);
            if (!string.IsNullOrEmpty(token))
                requestUrl += string.Format("&token={0}", token);

            WebRequest req = WebRequest.Create(requestUrl);
            req.ContentType = file.ContentType;
            req.Method = "POST";
            req.ContentLength = file.FileData.Length;
            var credentials = new System.Net.CredentialCache
                                  {
                                      {
                                          new System.Uri(_zenDeskUrl), "Basic",
                                          new System.Net.NetworkCredential(_user, _password)
                                          }
                                  };
            
            req.Credentials = credentials;                        
            req.PreAuthenticate = true;
            req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequired;
            var dataStream = req.GetRequestStream();
            dataStream.Write(file.FileData, 0, file.FileData.Length);
            dataStream.Close();

            WebResponse response = req.GetResponse();
            dataStream = response.GetResponseStream();            
            var reader = new StreamReader(dataStream);            
            string responseFromServer = reader.ReadToEnd();

            return GetAttachmentToken(responseFromServer);           
        }
                
        static string GetAttachmentToken(string xml)
        {
            string token = string.Empty;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                token = doc.DocumentElement.Attributes[0].Value;
            }
            catch(Exception)
            { }

            return token;
        }
    }
}
