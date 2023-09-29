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
        //-------------Post
        List<Post> GetPosts { get; }  
        Post GetPost(int id);
        //NOW WE WILL IMPLEMENT ADD, UPDATE AND DELETE FUNCTIONALITY
        void CreatePost(Post post); 
        void UpdatePost(Post post); 
        void DeletePost(int id); 
    
        
        //-------------Category
                List<Category> GetCategories();
        Category GetCategory (int id);
        void AddUpdateCategory(Category category);
        void DeleteCategory(int id);
        //-------------PostStatuses Methods
        List<PostStatus> GetPostStatuses();
        PostStatus GetPostStatus(int id);
        void AddUpdatePostStatus(PostStatus postStatus);
        void DeletePostStatus(int id);


        

    }
}
