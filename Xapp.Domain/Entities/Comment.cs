using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class Comment : Entity
    {

        public string Content { get; private set; }
        public int Likes { get; private set; }
        public int PostId { get; private set; }
        public virtual Post Post { get; private set; } 
        public int UserId { get; private set; }
        public virtual User User { get; private set; }
    }
}