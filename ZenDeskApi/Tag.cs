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
        private const string Tags = "tags";

        public List<TagScore> GetAllTags()
        {
            return GetCollection<TagScore>(Tags);
        }
      
        public List<Ticket> GetAllTicketsForTag(string tagName)
        {
            var tickets = new List<Ticket>();

            try
            {
                int page = 1;
                var ticks = new List<Ticket>();

                //Try getting the tickets for all of the pages
                while (page == 1 || ticks.Count > 0)
                {
                    ticks = GetTicktesForTagByPage(tagName, page);
                    tickets.AddRange(ticks);

                    page++;
                }
            }
            //There were no more pages so just go on
            catch (ArgumentNullException ex)
            { }

            return tickets;        
        }

        /// <summary>
        /// Returns tickets with that tag in groups of 15. Use the page index to get the next set.
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<Ticket> GetTicktesForTagByPage(string tagName, int page = 1)
        {
        
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.json", Tags, tagName)
            };

            request.AddParameter("page", page.ToString());

            return Execute<List<Ticket>>(request);            
        
        }
    }
}
