using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenDeskApi.XmlSerializers;

namespace ZenDeskApi.Model
{
    [ZenDeskSerialization(AlternateName = "record")]
    public class TicketField
    {
        public int AccountId { get; set; }
        public string CreatedAt { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditableInPortal { get; set; }
        public bool IsRequired { get; set; }
        public bool IsRequiredInPortal { get; set; }
        public bool IsVisibleInPortal { get; set; }
        public int Position { get; set; }
        public string RegExpForValidation { get; set; }
        public int SubTypeId { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string TitleInPortal { get; set; }
        public string Type { get; set; }
        public string UpdatedAt { get; set; }
    }
}
