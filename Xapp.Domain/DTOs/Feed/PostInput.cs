using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xapp.Domain.DTOs
{
    public class PostInput
    {
        //Post
        public string Title { get; set; }
        public string Content { get; set; }
        public string Multimedia { get; set; }
        public Enums.Tags Tag { get; set; }
    }
}
