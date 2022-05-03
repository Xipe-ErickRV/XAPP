using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class PTOInput
    {
        public int UserId { get; set; }
        public bool IsPTO { get; set; }
        public bool IsVacation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
