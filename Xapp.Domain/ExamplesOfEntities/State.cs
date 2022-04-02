using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class State : Entity //This is a class that have a foreign key from another class wich is country
    {
        //properties o class
        public string Name { get; set; }
        public string Description { get; set; }

        //this is a foreign key 
        public int CountryId { get; set; } //the key in database
        public virtual Country Country { get; set; } //a space that will be use to store de entire object if we needed
        //------
    }
}
