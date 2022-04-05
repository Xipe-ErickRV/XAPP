using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class Post : Entity
    {
   
        public string Title { get; private set; }
        public string content { get; private set; }
        public string multimedia { get; private set; }
        public Tags Tags { get; set; }
        public int Likes { get; set; }
        public int UserID { get; private set; }
        public virtual User User { get; private set; }
        public List<Comment> Comments { get; private set; }
    }
}