using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class UserInput
    {
        //user
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //profile
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Area { get; set; }
        public string Bio { get; set; }
        public DateTime FechaCumple { get; set; }



    }
}
