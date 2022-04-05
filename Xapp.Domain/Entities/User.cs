using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class User: Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public List<PTO>  PTOs { get; set; }

        public List<evento> eventos { get; set; }

    }
}
