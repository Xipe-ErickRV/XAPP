using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Entities;

namespace Xapp.Domain.DTOs.Perfil
{
    public class ProfileOutput
    {
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Area { get; set; }
        public string Bio { get; set; }
        public DateTime FechaCumple { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
