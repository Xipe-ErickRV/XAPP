using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class Transfer : Entity
    {
        public int Receiver { get; set; }
        public int Sender { get; set; }

        //Declarar la llave fóranea
        public int WalletId { get; set; }
        public virtual Wallet Wallet { get; set; } //a space that will be use to store de entire object if we needed

    }
}
