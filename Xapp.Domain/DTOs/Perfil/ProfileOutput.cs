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
        public string UrlImage { get; set; }
        public string UrlCV { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime FechaCumple { get; set; }
        public List<SkillInput> Skills { get; set; }
    }
}
