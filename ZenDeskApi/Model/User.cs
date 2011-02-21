using System.Collections.Generic;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(Name="user")]
    public class User
    {
        [ZenDeskSerialization(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerialization(Name = "locale-id", Skip = true)]
        public string LocaleId { get; set; }

        [ZenDeskSerialization(Name = "photo-url", Skip=true)]
        public string PhotoUrl { get; set; }

        [ZenDeskSerialization(Name = "openid-url", Skip = true)]
        public string OpenidUrl { get; set; }

        [ZenDeskSerialization(Name = "created-at", Skip = true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerialization(Name = "last-login", Skip = true)]
        public string LastLogin { get; set; }

        [ZenDeskSerialization(Name = "details")]
        public string Details { get; set; }

        [ZenDeskSerialization(Name = "updated-at", Skip = true)]
        public string UpdatedAt { get; set; }

        [ZenDeskSerialization(Name = "notes")]
        public string Notes { get; set; }

        [ZenDeskSerialization(Name = "email")]
        public string Email { get; set; }

        [ZenDeskSerialization(Name = "external-id", Skip = true)]
        public string ExternalId { get; set; }

        [ZenDeskSerialization(Name = "restriction-id")]
        public int? RestrictionId { get; set; }

        [ZenDeskSerialization(Name = "id")]
        public int? Id { get; set; }

        [ZenDeskSerialization(Name = "phone")]
        public string Phone { get; set; }

        [ZenDeskSerialization(Name = "is-active", Skip=true)]
        public bool IsActive { get; set; }

        [ZenDeskSerialization(Name = "is-verified")]
        public bool IsVerified { get; set; }

        [ZenDeskSerialization(Name = "time-zone", Skip = true)]
        public string TimeZone { get; set; }

        [ZenDeskSerialization(Name = "roles")]
        public int? Role { get; set; }

        [ZenDeskSerialization(Name = "organization-id", Skip = true)]
        public int? OrganizationId { get; set; }

        [ZenDeskSerialization(Name = "groups", Skip = true)]
        public List<Group> Groups { get; set; }

        [ZenDeskSerialization(Name = "uses-12-hour-clock")]
        public bool? Uses12HourClock { get; set; }

        [ZenDeskSerialization(Name = "password")]
        public string Password { get; set; }

        [ZenDeskSerialization(Name = "groups", ListItemName="group")]
        public List<int> GroupIds { get; set; }       
    }

    public enum Role
    {
        EndUser = 0,
        Administrator = 2,
        Agent = 4
    }

    public enum RestrictedTo
    {
        AllTickets,
        TicketsInMemberGroup,
        TicketsInMemberOrganization,
        AssignedTickets,
        TicketsRequestedByUser
    }
}
