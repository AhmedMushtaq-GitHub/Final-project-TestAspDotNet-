using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAspDotNet.Model;
using TestAspDotNet.Repository;

namespace TestAspDotNet.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _post;
        private readonly IAccount _account;
        public PostController(IPost post, IAccount account)
        {
            _post = post;
            _account = account;
        }
        //-------------Post
        [Admin]
        [HttpGet]
        public IActionResult GetPosts()
        {
            return View(_post.GetPosts);
        }
        
        [HttpGet]
        public IActionResult DetailsPost(int id)
        {
            return View(_post.GetPost(id));
        }

        //----------------Categories

        [Admin]
        [HttpGet]
        public IActionResult Categories() 
        {
            return View(_post.GetCategories());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateCategory(int id = 0)
        {
            return View(_post.GetCategory(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateCategory(Category category)
        {
            _post.AddUpdateCategory(category);
            return RedirectToAction("Categories");
        }
        [Admin]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
           _post.DeleteCategory(id);
            return RedirectToAction("Categories");
        }

        //----------------PostStatuses

       
        [HttpGet]
        public IActionResult PostStatuses()
        {
            return View(_post.GetPostStatuses());
        }
        
        [HttpGet]
        public IActionResult AddUpdatePostStatus(int id = 0)
        {
            return View(_post.GetPostStatus(id));
        }
        
        [HttpPost]
        public IActionResult AddUpdatePostStatus(PostStatus postStatus)
        {
            _post.AddUpdatePostStatus(postStatus);
            return RedirectToAction("PostStatuses");
        }
        
        [HttpGet]
        public IActionResult DeletePostStatus(int id)
        {
            _post.DeletePostStatus(id);
            return RedirectToAction("PostStatuses");
        }

        //-----------post crud Methods
        [HttpGet]
        public IActionResult CreatePost()
        {
            ViewBag.PostStatus = new SelectList(_post.GetPostStatuses().ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            return View(new Post());
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            User user = new CommonController(_account).GetUser(HttpContext);
            post.UserId = user.Id;
            
            _post.CreatePost(post);
            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult UpdatePost(int id = 0)
        {
            ViewBag.PostStatus = new SelectList(_post.GetPostStatuses().ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            return View(_post.GetPost(id));
        }

        [HttpPost]
        public IActionResult UpdatePost(Post post)
        {
            _post.UpdatePost(post);
            return RedirectToAction("GetPosts");
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            _post.DeletePost(id);
            return RedirectToAction("GetPosts");

        }


    }
}
