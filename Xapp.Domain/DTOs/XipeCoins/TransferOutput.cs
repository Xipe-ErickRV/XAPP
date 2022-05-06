using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class TransferOutput
    {
        public decimal Amount { get; set; }
        public int IdReceiver { get; set; }
        public string NameReceiver { get; set; }    
        public int IdSender { get; set; }
        public string NameSender { get; set; }
        public string AmountConcept { get; set; }

        public DateTime DateTime { get; set; }

    }
}

