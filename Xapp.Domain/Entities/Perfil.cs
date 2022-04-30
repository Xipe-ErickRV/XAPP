using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.DTOs.Perfil;

namespace Xapp.Domain.Entities
{
    public class Perfil
    {
        [ForeignKey("User")]
        public int Id { get; set; } //Perfil and User use the same id
        
        public string Telefono { get;  set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Area { get; set; }
        public string Bio { get; set; }
        public string UrlFoto { get; set; }
        public string UrlCv { get; set; }
        public DateTime FechaCumple { get; set; }


        public List<Skill> Skills { get; set; }
        public List<Event> Eventos { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public void MetodoEdit(ProfileUpdate dto)
        {
            Nombre = dto.Nombre;
            Apellido = dto.Apellido;
            Telefono = dto.Telefono;
            FechaCumple = dto.FechaCumple;
            Area = dto.Area;
            Bio = dto.Bio;
        }
        public void CreateEntity()
        {
            CreationDate = DateTime.Now;
            EditEntity();
            IsActive = true;
            IsDeleted = false;
        }
        public void EditEntity()
        {
            LastUpdate = DateTime.Now;
        }

        public void ChangeStatus()
        {
            IsActive = !IsActive;
            EditEntity();
        }

        public void Delete()
        {
            IsActive = false;
            IsDeleted = true;
            EditEntity();
        }
        public virtual User User { get; set; }

        public ProfileOutput Output()
        {
            return new ProfileOutput
            {
                Telefono = Telefono,
                Nombre = Nombre,
                Apellido = Apellido,
                Area = Area,
                Bio = Bio,
                Email = User.Email,
                FechaCumple = FechaCumple,
                Skills = Skills
            };

        }

    }
}
