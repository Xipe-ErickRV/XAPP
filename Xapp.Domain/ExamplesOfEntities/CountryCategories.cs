using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    //This class is an example of intermediate tables, in this example here we are going to storage a single country with a single category but it cold be storage many of them, so, one country cold have many categories
    //(see the class 'country' to see the virtual list of categories that could have)
    public class CountryCategories : Entity 
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
