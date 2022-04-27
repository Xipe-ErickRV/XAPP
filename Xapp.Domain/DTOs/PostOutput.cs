using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xapp.Domain.Entities;

namespace Xapp.Domain.DTOs
{
    public class PostOutput
    {
        //Post
        public string Title { get; set; }
        public string Content { get; set; }
        public string Multimedia { get; set; }
        public Enums.Tags Tag { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string URLProfile { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
