using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name = "ticket")]
    public class Ticket
    {                
        [ZenDeskSerializeAs(Name = "assigned-at")]
        public string AssignedAt { get; set; }

        [ZenDeskSerializeAs(Name = "assignee-id")]
        public string AssigneeId { get; set; }

        [ZenDeskSerializeAs(Name = "assignee-updated-at")]
        public string AssigneeUpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "created-at")]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "subject")]
        public string Subject { get; set; }

        [ZenDeskSerializeAs(Name = "description")]
        public string Description { get; set; }

        [ZenDeskSerializeAs(Name = "external-id")]
        public int? ExternalId { get; set; }

        [ZenDeskSerializeAs(Name = "group-id")]
        public int? GroupId { get; set; }

        [ZenDeskSerializeAs(Name = "id")]
        public int NiceId { get; set; }

        [ZenDeskSerializeAs(Name = "linked-id")]
        public int? LinkedId { get; set; }

        [ZenDeskSerializeAs(Name = "priority-id")]
        public int PriorityId { get; set; }

        [ZenDeskSerializeAs(Name = "submitter-id")]
        public int SubmitterId { get; set; }

        [ZenDeskSerializeAs(Name = "status-id")]
        public int StatusId { get; set; }

        [ZenDeskSerializeAs(Name = "status-updated-at")]
        public string StatusUpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "requester-id")]
        public int RequesterId { get; set; }

        [ZenDeskSerializeAs(Name = "requester-updated-at")]
        public string RequesterUpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "ticket-type")]
        public string TicketType { get; set; }

        [ZenDeskSerializeAs(Name = "updated-at")]
        public string UpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "via-id", Skip=true)]
        public int ViaId { get; set; }

        [ZenDeskSerializeAs(Name="set-tags")]
        public string SetTags { get; set; }

        [ZenDeskSerializeAs(Name = "current-tags")]
        public string CurrentTags { get; set; }

        [ZenDeskSerializeAs(Name = "score", Skip=true)]
        public int Score { get; set; }

        [ZenDeskSerializeAs(Name = "comments")]
        public List<Comment> Comments { get; set; }

        [ZenDeskSerializeAs(Name = "ticket-field-entries")]
        public List<TicketFieldEntry> TicketFieldEntries { get; set; }

        /// <summary>
        /// Note: This is only used for Creating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerializeAs(Name = "requester-name")]        
        public string RequesterName { get; set; }

        /// <summary>
        /// Note: This is only used for Creating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerializeAs(Name = "requester-email")]        
        public string RequesterEmail { get; set; }

        /// <summary>
        /// Note: This is only used for Updating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerializeAs(Name = "additional-tags")]        
        public string AdditionalTags { get; set; }
    }

    [ZenDeskSerializeAs(Name = "ticket-field-entry")]
    public class TicketFieldEntry
    {
        [ZenDeskSerializeAs(Name = "ticket-field-id")]
        public int TicketFieldId { get; set; }

        [ZenDeskSerializeAs(Name = "value")]
        public bool Value { get; set; }
    }

    public enum TicketStatus
    {
        New,
        Open,
        Pending,
        Solved,
        Closed
    }

    public enum TicketType
    {
        NoTypeSet,
        Question,
        Incident,
        Problem,
        Task
    }

    public enum TicketPriorities
    {
        NoPrioritySet,
        Low,
        Normal,
        High,
        Urgent

    }

    public enum TicketViaType
    {
        WebForm = 0,
        Mail = 4,
        WebServiceOrApi = 5,
        GetSatisfaction = 16,
        Dropbox = 17,
        TicketMerge = 19,
        RecoveredFromSuspendedTickets = 21,
        TwitterFavorite = 23,
        ForumTopic = 24,
        TwitterDirectMessage = 26,
        ClosedTicket = 27,
        Chat = 29,
        TwitterPublicMention = 30

    }
}
