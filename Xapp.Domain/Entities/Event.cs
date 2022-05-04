using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;

namespace Xapp.Domain.Entities
{
    public class Event : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public bool IsPublic { get; set; }

        public bool IsPublish { get; set; }



        public EventInput Output()
        {
            return new EventInput
            {
                Title = Title,
                Description = Description,
                Date = DateTime,
                IsPublic = IsPublic,
                UserId = UserId,
                EventId = Id
            };
        }
    }
}
