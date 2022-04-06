using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class Perfil
    {
        [ForeignKey("User")]
        public int IdUser { get; private set; } //Perfil and User use the same id
        
        public string Telefono { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Area { get; private set; }
        public string Bio { get; private set; }
        public string UrlFoto { get; private set; }
        public string UrlCv { get; private set; }
        public DateTime FechaCumple { get; private set; }


        public List<Skill> Skills { get; set; }
        public List<Evento> Eventos { get; set; }

        public DateTime CreationDate { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

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
    }
}
