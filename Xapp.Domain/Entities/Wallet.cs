using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.Entities
{
    public class Wallet : Entity
    {
        public int Balance { get; private set; }
    }
}
