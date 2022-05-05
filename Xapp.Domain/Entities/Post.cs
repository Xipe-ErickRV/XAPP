using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.Enums;

namespace Xapp.Domain.Entities
{
    public class Post : Entity
    {
   
        public string Title { get; set; }
        public string Content { get; set; }
        public string Multimedia { get; set; }
        public Tags Tag { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Comment> Comments { get; set; }


       
    }
}