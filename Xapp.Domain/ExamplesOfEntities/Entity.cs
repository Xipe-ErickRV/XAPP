using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class Entity
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; } //This Id will be inherited by all the entities that inherit from here, works like unique identifier (primary key)

        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow; //This CreationDate will be inherited by all the entities that inherit from here and default value is Datetime now

        [ScaffoldColumn(false)]
        public DateTime LastUpdate { get; set; }

        [ScaffoldColumn(false)]
        public Boolean IsDeleted { get; set; } //This IsDeleted will be inherited by all the entities that inherit from here and this is the logical deleted
    }
}
