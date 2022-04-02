using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.ExamplesOfEntities
{
    public class Category : Entity //This class is a catalogue, like categories xd
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
