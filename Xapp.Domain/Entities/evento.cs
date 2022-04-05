using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class evento : Entity
    {
        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public bool  Is_public { get; set; }

        public bool Is_publish { get; set; }


    }
}
