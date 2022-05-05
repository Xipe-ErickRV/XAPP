using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class Wallet : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int Balance { get; set; }

        public virtual List<Transfer> Transfers{ get; set; }

        public void Sum(int amount)  //Sumar transferencia a balance
        {
            Balance += amount;
        }

        public void Sub(int amount) //Restar al balance
        {
            Balance -= amount;
        }
    }
}
