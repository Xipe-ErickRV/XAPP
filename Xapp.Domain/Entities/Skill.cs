using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class Skill : Entity
    {
        [ForeignKey("User")]
        public int User { get; set; }
        public string Nombre { get; set; }
        public Nivel Nivel { get; set; }
        public string Descripcion { get; set; }

        public SkillInput ToOutput()
        {
            return new SkillInput
            {
                Nombre = Nombre,
                Nivel = Nivel
            };
        }
    }
}
