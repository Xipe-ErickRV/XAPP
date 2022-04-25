using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class CommentInput
    {
        //Comment
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
