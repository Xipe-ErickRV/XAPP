using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public virtual List<PTO> PTOs { get; set; }
        public virtual List<Rol> Roles { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual Perfil PerfilUser { get; set; }
        public virtual Wallet WalletlUser { get; set; }


        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

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
