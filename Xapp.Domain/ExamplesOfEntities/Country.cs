using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class Country : Entity //This class is a "main" class, there is no have foreign key at the moment
    {
        //properties o class
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<CountryCategories> Categories { get; set; } //list of categories thath have this single country

    }
}
