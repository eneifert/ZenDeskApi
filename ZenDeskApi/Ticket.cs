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
        private const string Tickets = "tickets";
        private const string Requests = "requests";

        /// <summary>
        /// The first comment for a ticket is always equivalent to the ticket description.
        /// If you have any custom fields in your zendesk, they will show up in the <ticket-field-entries> part of the document.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetTicketById(int id)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.json", Tickets, id)
            };

            return Execute<Ticket>(request); 
        }

        public List<Ticket> GetAllTicketsForUser(string email)
        {
            var tickets = new List<Ticket>();

            try
            {
                int page = 1;
                var ticks = new List<Ticket>();

                //Try getting the tickets for all of the pages
                while (page == 1 || ticks.Count > 0)
                {
                    ticks = GetTicketsForUserByPage(email, page);
                    tickets.AddRange(ticks);

                    page++;
                }
            }
            //There were no more pages so just go on
            catch (ArgumentNullException ex)
            { }

            return tickets;
        }

        public List<Ticket> GetTicketsForUserByPage(string email, int page=1)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = Requests + ".json",
            };

            //Assume the user
            request.AddHeader(XOnBehalfOfEmail, email);
            request.AddParameter("page", page.ToString());

            //Get the open ones
            var ticktes = Execute<List<Ticket>>(request);

            //Now get the closed ones
            request.AddParameter("filter", "solved");
            var closedOrSolved = Execute<List<Ticket>>(request);

            ticktes.AddRange(closedOrSolved);
            return ticktes;
        }

        public List<Ticket> GetTicketsInViewByPage(int viewId, int page=1)
        {
            var request = new ZenRestRequest
            {
                Method = Method.GET,
                Resource = string.Format("rules/{0}.json", viewId)
            };

            request.AddParameter("page", page.ToString());

            return Execute<List<Ticket>>(request);
        }

        public List<Ticket> GetAllTicketsInView(int viewId)
        {          
            var tickets = new List<Ticket>();

            try
            {
                int page = 1;
                var ticks = new List<Ticket>();

                //Try getting the tickets for all of the pages
                while(page == 1 || ticks.Count > 0)
                {
                    ticks = GetTicketsInViewByPage(viewId, page);                                        
                    tickets.AddRange(ticks);

                    page++;
                }
            }
            //There were no more pages so just go on
            catch(ArgumentNullException ex)
            { }

            return tickets;            
        }

        public int CreateTicketAsEndUser(string endUserEmail, string subject, string description)
        {
            return CreateTicketAsEndUser(endUserEmail, new Ticket {Subject = subject, Description = description});
        }

        public int CreateTicketAsEndUser(string endUserEmail, Ticket ticket)
        {
            var request = new ZenRestRequest
            {
                Method = Method.POST,
                Resource = Requests + ".xml"
            };

            request.AddHeader(XOnBehalfOfEmail, endUserEmail);
            request.AddBody(ticket);

            //request.AddParameter("text/xml", string.Format("<ticket><subject>{0}</subject><description>{1}</description></ticket>", ticket.Subject, ticket.Description), ParameterType.RequestBody);

            var res = Execute(request);
            return GetIdFromLocationHeader(res);
        }

        public bool UpdateTicket(int ticketId, string description)
        {
            return UpdateTicket(ticketId, new Comment {Value = description});
        }

        public bool UpdateTicket(int ticketId, Comment comment)        
        {
            string email = GetUserById(GetTicketById(ticketId).RequesterId).Email;

            var request = new ZenRestRequest
            { 
                Method = Method.PUT, 
                Resource = string.Format("{0}/{1}.xml", Requests, ticketId.ToString())                
            };

            request.AddHeader(XOnBehalfOfEmail, email);
            request.AddBody(comment);

            //request.AddParameter("text/xml", string.Format("<ticket><subject>{0}</subject><description>{1}</description></ticket>", ticket.Subject, ticket.Description), ParameterType.RequestBody);

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool DestroyTicket(int ticketId)
        {         
            var request = new ZenRestRequest
            {
                Method = Method.DELETE,
                Resource = string.Format("{0}/{1}.xml", Requests, ticketId.ToString())  
            };

            var res = Execute(request);

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
