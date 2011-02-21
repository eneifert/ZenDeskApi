using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name = "ticket")]
    public class Ticket
    {                
        [ZenDeskSerialization(Name = "assigned-at", Skip=true)]
        public string AssignedAt { get; set; }

        [ZenDeskSerialization(Name = "assignee-id")]
        public string AssigneeId { get; set; }

        [ZenDeskSerialization(Name = "assignee-updated-at", Skip = true)]
        public string AssigneeUpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "created-at", Skip = true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "subject")]
        public string Subject { get; set; }

        [ZenDeskSerialization(Name = "description")]
        public string Description { get; set; }

        [ZenDeskSerialization(Name = "external-id")]
        public int? ExternalId { get; set; }

        [ZenDeskSerialization(Name = "group-id")]
        public int? GroupId { get; set; }

        [ZenDeskSerialization(Name = "id")]
        public int NiceId { get; set; }

        [ZenDeskSerialization(Name = "linked-id")]
        public int? LinkedId { get; set; }

        [ZenDeskSerialization(Name = "priority-id")]
        public int PriorityId { get; set; }

        [ZenDeskSerialization(Name = "submitter-id")]
        public int SubmitterId { get; set; }

        [ZenDeskSerialization(Name = "status-id")]
        public int StatusId { get; set; }

        [ZenDeskSerialization(Name = "status-updated-at", Skip = true)]
        public string StatusUpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "requester-id")]
        public int RequesterId { get; set; }

        [ZenDeskSerialization(Name = "requester-updated-at", Skip = true)]
        public string RequesterUpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "ticket-type-id")]
        public int TicketTypeId { get; set; }

        [ZenDeskSerialization(Name = "updated-at", Skip = true)]
        public string UpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "via-id", Skip=true)]
        public int ViaId { get; set; }

        [ZenDeskSerialization(Name="set-tags")]
        public string SetTags { get; set; }

        [ZenDeskSerialization(Name = "current-tags")]
        public string CurrentTags { get; set; }

        [ZenDeskSerialization(Name = "score", Skip=true)]
        public int Score { get; set; }

        [ZenDeskSerialization(Name = "comments")]
        public List<Comment> Comments { get; set; }

        [ZenDeskSerialization(Name = "ticket-field-entries")]
        public List<TicketFieldEntry> TicketFieldEntries { get; set; }

        /// <summary>
        /// Note: This is only used for Creating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerialization(Name = "requester-name")]        
        public string RequesterName { get; set; }

        /// <summary>
        /// Note: This is only used for Creating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerialization(Name = "requester-email")]        
        public string RequesterEmail { get; set; }

        /// <summary>
        /// Note: This is only used for Updating tickets and this field is not returned for getting tickets
        /// </summary>
        [ZenDeskSerialization(Name = "additional-tags")]        
        public string AdditionalTags { get; set; }
    }

    [ZenDeskSerialization(Name = "ticket-field-entry")]
    public class TicketFieldEntry
    {
        [ZenDeskSerialization(Name = "ticket-field-id")]
        public int TicketFieldId { get; set; }

        [ZenDeskSerialization(Name = "value")]
        public string Value { get; set; }
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
