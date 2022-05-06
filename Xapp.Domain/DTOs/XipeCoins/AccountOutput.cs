using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class AccountOutput
    {
        public int Balance { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlFoto { get; set; }

        public decimal Amount { get; set; }
        public int Receiver { get; set; }
        public int Sender { get; set; }
        public string AmountConcept { get; set; }

        public DateTime DateTime { get; set; }
        public List<TransferOutput> TransferOutputs { get; set; }

    }
}

