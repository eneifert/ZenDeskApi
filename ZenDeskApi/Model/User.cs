using System.Collections.Generic;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerializeAs(Name="user")]
    public class User
    {
        [ZenDeskSerializeAs(Name = "name")]
        public string Name { get; set; }

        [ZenDeskSerializeAs(Name = "locale-id", Skip = true)]
        public string LocaleId { get; set; }

        [ZenDeskSerializeAs(Name = "photo-url", Skip=true)]
        public string PhotoUrl { get; set; }

        [ZenDeskSerializeAs(Name = "openid-url", Skip = true)]
        public string OpenidUrl { get; set; }

        [ZenDeskSerializeAs(Name = "created-at", Skip = true)]
        public string CreatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "last-login", Skip = true)]
        public string LastLogin { get; set; }

        [ZenDeskSerializeAs(Name = "details")]
        public string Details { get; set; }

        [ZenDeskSerializeAs(Name = "updated-at", Skip = true)]
        public string UpdatedAt { get; set; }

        [ZenDeskSerializeAs(Name = "notes")]
        public string Notes { get; set; }

        [ZenDeskSerializeAs(Name = "email")]
        public string Email { get; set; }

        [ZenDeskSerializeAs(Name = "external-id", Skip = true)]
        public string ExternalId { get; set; }

        [ZenDeskSerializeAs(Name = "restriction-id")]
        public int? RestrictionId { get; set; }

        [ZenDeskSerializeAs(Name = "id")]
        public int? Id { get; set; }

        [ZenDeskSerializeAs(Name = "phone")]
        public string Phone { get; set; }

        [ZenDeskSerializeAs(Name = "is-active", Skip=true)]
        public bool IsActive { get; set; }

        [ZenDeskSerializeAs(Name = "is-verified")]
        public bool IsVerified { get; set; }

        [ZenDeskSerializeAs(Name = "time-zone", Skip = true)]
        public string TimeZone { get; set; }

        [ZenDeskSerializeAs(Name = "roles")]
        public int? Role { get; set; }

        [ZenDeskSerializeAs(Name = "organization-id", Skip = true)]
        public int? OrganizationId { get; set; }

        [ZenDeskSerializeAs(Name = "groups", Skip = true)]
        public List<Group> Groups { get; set; }

        [ZenDeskSerializeAs(Name = "uses-12-hour-clock")]
        public bool? Uses12HourClock { get; set; }

        [ZenDeskSerializeAs(Name = "password")]
        public string Password { get; set; }

        [ZenDeskSerializeAs(Name = "groups", ListItemName="group")]
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
