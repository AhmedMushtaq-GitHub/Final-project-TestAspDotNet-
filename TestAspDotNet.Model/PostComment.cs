using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAspDotNet.Model
{
    public class PostComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentedOn { get; set; }
        public User User { get; set; }
        public virtual int UserId { get; set; }

        public Post Post { get; set; }
        public virtual int PostId { get; set; }
    }
}
