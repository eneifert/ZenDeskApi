using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZenDeskApi.Model
{
    public class View
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int OwnerId { get; set; }
        public string OwnerType { get; set; }
        public int PerPage { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public Output Output { get; set; }
    }

    public class Output
    {
        public string List { get; set; }
        public string Order { get; set; }
        public List<Column> Columns { get; set; }
        public string Group { get; set; }
        public bool OrderAsc { get; set; }
    }

    public class Column
    {
        public string Name { get; set; }
    }
}
