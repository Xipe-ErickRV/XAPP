using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class CommentOutput
    {
        //Comment
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string URLProfile { get; set; }
        public int Likes { get; set; }
    }
}
