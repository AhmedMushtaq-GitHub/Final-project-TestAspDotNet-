using Microsoft.AspNetCore.Mvc;
using TestAspDotNet.Repository;

namespace TestAspDotNet.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _post;
        public PostController(IPost post)
        {
            _post = post;
        }
        [Admin]
        [HttpGet]
        public IActionResult GetPosts()
        {
            return View(_post.GetPosts);
        }
        [Admin]
        [HttpGet]
        public IActionResult DetailsPost(int id)
        {
            return View(_post.GetPost(id));
        }
    }
}
