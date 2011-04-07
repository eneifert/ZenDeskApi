using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RestSharp;
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
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = "uploads.xml"
            };
            request.AddParameter("filename", file.FileName);
            if (!string.IsNullOrEmpty(token))
                request.AddParameter("token", token);

            request.AddParameter("data-binary", file.FileData);

            var res = Execute(request);

            return GetAttachmentToken(res.Content);
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
