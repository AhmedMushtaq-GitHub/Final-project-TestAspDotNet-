using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository
{
    public interface IPost
    {
        List<Post> GetPosts { get; }  
        Post GetPost(int id);
    }
}
