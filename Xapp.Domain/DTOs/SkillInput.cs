using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;

namespace Xapp.Domain.DTOs
{
    public class SkillInput
    {
        public string Nombre { get; set; }
        public Nivel Nivel { get; set; }
        public string Descripcion { get; set; }
    }
}