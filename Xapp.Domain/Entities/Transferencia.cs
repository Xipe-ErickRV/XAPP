using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
namespace Xapp.Domain.Wallet
{
    public class Transferencia : Entity
    {
        public int c_in_Recibo { get; private set; }
        public int c_in_Envio { get; private set; }

        //Declarar la llave fóranea
        public int id_Wallet { get; set; }
        public virtual Wallet Wallet { get; set; } //a space that will be use to store de entire object if we needed

    }
}
