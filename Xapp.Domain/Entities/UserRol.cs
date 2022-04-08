using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class UserRol:Entity
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int RolID { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
