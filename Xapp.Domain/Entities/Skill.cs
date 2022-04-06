using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class Skill : Entity
    {
        public string Nombre { get; private set; }
        public Nivel Nivel { get; private set; }
        public string Descripcion { get; private set; }
    }
}
