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
        public List<View> GetViews()
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = "views.json",
                RootElement = "views"
            };

            return Execute<List<View>>(request); 
        }

        public View GetViewByName(string name)
        {                        
            return GetViews().FirstOrDefault(v => v.Title == name);
        }
    }
}
