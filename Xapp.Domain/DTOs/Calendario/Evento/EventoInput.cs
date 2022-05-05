using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class EventInput
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public DateTime Date { get; set; }
    }

    public class EventList
    {
        public List<EventInput> Events { get; set; }
    }
}
