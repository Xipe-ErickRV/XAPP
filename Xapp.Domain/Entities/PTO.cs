using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;
using Xapp.Domain.DTOs;

namespace Xapp.Domain.Entities
{
    public class PTO : Entity
    {
        public bool IsPTO { get; set; }
        public bool IsVacation { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public PTOInput Output()
        {
            return new PTOInput
            {
                IsPTO = IsPTO,
                IsVacation = IsVacation,
                StartTime = StartDate,
                EndTime = EndDate,
                Description = Description,
                UserId = UserId
            };
        }
    }
}
