﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Drawing;
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
        public IActionResult CreatePost(Post post , IFormFile PostImage)
        {
            string imagePath = "";
            var extention = "";
            IList<string> AllowedFileExtenstions = new List<string> {".jpg" ,".jpeg", ".png" };
            int maxContentLength = 1024 * 1024 * 10; //10 Mb
            if (PostImage != null && PostImage.Length > 0)
            {

                extention = PostImage.FileName.Substring(PostImage.FileName.LastIndexOf('.')).ToLower();
                if (PostImage.Length > maxContentLength)
                {
                    ViewBag.Error = "File size  must be less then 10mb";
                }
                else if (!AllowedFileExtenstions.Contains(extention))
                {
                    ViewBag.Error = "Please upload img in .jpg .jpeg .png";
                }
                else
                {
                   
                        string relativeimgpath = $"/images/posts/{post.Id}-{Path.GetFileNameWithoutExtension(PostImage.FileName)}-{DateTime.UtcNow.Ticks}.jpg";
                        string absoluteimgpath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{relativeimgpath}");
                        using (var stream = new FileStream(absoluteimgpath, FileMode.Create))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                PostImage.CopyTo(memoryStream);
                                using (var img = Image.FromStream(memoryStream))
                                {
                                    int width = img.Width;
                                    int height = img.Height;
                                    if (width > 800 || height > 800)
                                    {
                                        ViewBag.Error = "Plese upload img with dimension 700*500 or less";
                                    }
                                    else
                                    {
                                        PostImage.CopyTo(stream);
                                        post.Image = relativeimgpath;
                                        var user = new CommonController(_account).GetUser(HttpContext);
                                        post.UserId = user.Id;
                                        try
                                        {
                                            _post.CreatePost(post);
                                            return RedirectToAction("Getposts");
                                        }
                                        catch (Exception ex)
                                        {
                                            ViewBag.Error = "an error occurred while saving the post to the Database";
                                            return View(post);
                                        }
                                    }
                                }
                            }
                        }
                    
                }

            }
            
            return View();
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
        //-------------Reaction Type
       
        [HttpGet]
        public IActionResult Reactiontypes()
        {
            return View(_post.GetReactionTypes());
        }
      
        [HttpGet]
        public IActionResult AddUpdateReactiontypes(int id = 0)
        {
            return View(_post.GetReactionType(id));
        }
        
        [HttpPost]
        public IActionResult AddUpdateReactiontypes(ReactionType reactionType)
        {
           _post.AddUpdateReactionType(reactionType);
            return RedirectToAction("Reactiontypes");
        }
      
        [HttpGet]
        public IActionResult DeleteReactiontypes(int id)
        {
           _post.DeleteReactionType(id);
            return RedirectToAction("Reactiontypes");
        }
        //----------post reaction
        [HttpGet]
        public IActionResult PostReactions()
        {
          return View(_post.GetPostReactions());
        }

    

        [HttpGet]
        public IActionResult GetPostReaction(int id = 0)
        {
            return View(_post.GetPostReaction(id));
        }
        [HttpGet]
        public IActionResult CreatePostReaction()
        {
            ViewBag.Posts = new SelectList(_post.GetPosts.ToList(), "Id", "Title");
            ViewBag.Reactions = new SelectList(_post.GetReactionTypes().ToList(), "Id", "Name");
            return View(new PostReaction());
        }

        [HttpPost]
        public IActionResult CreatePostReaction(PostReaction postReaction)
        {
            User user = new CommonController(_account).GetUser(HttpContext);
            postReaction.UserId = user.Id;

            _post.CreatePostReaction(postReaction);
            return RedirectToAction("PostReactions");
        }

        [HttpGet]
        public IActionResult UpdatePostReaction(int id = 0)
        {
          ViewBag.Posts = new SelectList(_post.GetPostStatuses().ToList(), "Id", "Title");
            ViewBag.Reactions = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            return View (_post.GetPostReaction(id));


        }

        [HttpPost]
        public IActionResult UpdatePostReaction(PostReaction postReaction)
        {
           _post.UpdatePostReaction(postReaction);
            return RedirectToAction("PostReactions");
        }

       

        [HttpGet]
        public IActionResult DeletePostReaction(int id)
        {
            _post.DeletePostReaction(id);
            return RedirectToAction("PostReactions");
        }
    }
}
