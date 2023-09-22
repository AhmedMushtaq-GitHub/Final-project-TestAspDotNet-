using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Data;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository.Implementation
{
    public class PostRepository : IPost
    {
        private readonly TestAspDotNetDbContext _db;
        public PostRepository(TestAspDotNetDbContext db)
        {
            _db = db;
        }
        public List<Post> GetPosts
        {
            get { return _db.Posts.Include(x => x.Category).Include(x => x.PostStatus).ToList(); }
        }
     

        public Post GetPost(int id)
        {
            return _db.Posts.Where(x => x.Id.Equals(id)).Include(x => x.Category).Include(x => x.PostStatus).Include(x => x.User).FirstOrDefault();
        }
    }
}
