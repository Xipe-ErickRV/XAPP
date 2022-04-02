using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class ThisIsNotAUser
    {
        public string Id { get; set; } //string because most of times the id for a user is like 'QW2W23-DB2W23-PWLJ57-ZX8W46' for example and is shared with 1:1 relationship of UserInfo
        public string Email { get; set; }
        public string Password { get; set; } //this is secret xD
        public string EtcProperty { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ThisIsNotABasicInfo UserInfo { get; set; }

    }
}
