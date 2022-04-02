using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class ThisIsNotABasicInfo //As you can see this class is not inheriting from 'entity' because it is a 1:1 relationship so we have to add the missing properties like IsDeleted etc..
    {
        [ForeignKey("ThisIsNotAUser")] //this is a foreign key
        public string Id { get; set; } //this too 
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
