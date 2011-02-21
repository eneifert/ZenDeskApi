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
        private string _organizations = "organizations";

        public List<Organization> GetOgranizations()
        {
            return GetCollection<Organization>(_organizations);
        } 

        public Organization GetOgranizationById(int id)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.xml", _organizations, id)
            };

            return Execute<Organization>(request);            
        }      

        public int CreateOrUpdateOrganization(string name, string defaultSite)
        {
            return CreateOrUpdateOrganization(new Organization() { Name = name, Default = defaultSite });
        }

        public int CreateOrUpdateOrganization(Organization newOrg)
        {
            var orgs = GetOgranizations();
            var curOrg = orgs.Where(x => x.Name == newOrg.Name);

            if (curOrg.Count() > 0)
            {
                newOrg.Id = curOrg.First().Id;

                //If it couldn't be updated
                if (!UpdateOrganization(newOrg))
                    return -1;

                return curOrg.First().Id;
            }

            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = _organizations + ".xml"
            };

            request.AddBody(newOrg);

            var res = Execute(request);

            return GetIdFromLocationHeader(res);
        }

        public bool UpdateOrganization(Organization org)
        {
            var request = new ZenRestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("{0}/{1}.xml", _organizations, org.Id)
            };

            request.AddBody(org);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyOrganization(int orgId)
        {
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", _organizations, orgId)
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
