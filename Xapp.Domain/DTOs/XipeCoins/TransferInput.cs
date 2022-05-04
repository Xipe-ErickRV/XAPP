using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class TransferInput
    {
        public int Amount { get; set; }
        public int IdReceiver { get; set; }
        public int IdSender { get; set; }
        public string AmountConcept { get; set; }

    }
}

