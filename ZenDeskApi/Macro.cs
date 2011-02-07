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
        private const string Macros = "macros";

        public List<Macro> GetMacros()
        {
            return GetCollection<Macro>(Macros);
        }

        public bool ApplyMacro(int macroId, int ticketId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = string.Format("{0}/{1}/apply.xml", Macros, macroId)
            };

            request.AddParameter("ticket_id", ticketId.ToString());

            var resp = Execute(request);

            return resp.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
