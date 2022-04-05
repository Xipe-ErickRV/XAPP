using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class PTO : Entity
    {

        public Status Status { get; set; }
        public int UserId { get; private set; }
        public virtual User User { get; private set; }
    }
}
