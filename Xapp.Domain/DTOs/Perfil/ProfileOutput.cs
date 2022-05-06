using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Entities;

namespace Xapp.Domain.DTOs.Perfil
{
    public class ProfileOutput
    {
        public string UrlImage { get; set; }
        public string UrlCv { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime FechaCumple { get; set; }
        public List<Skill> Skills { get; set; }

        //[Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }

        public IFormFile Image { get; set; }
    }
}
