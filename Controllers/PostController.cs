using DemoBlogCore.Identity;
using DemoBlogCore.Models;
using DemoBlogCore.Viewmodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace DemoBlogCore.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _iwebhost;
        public PostController(ApplicationDbContext dbContext, IWebHostEnvironment iwebhost)
        {
            _dbContext = dbContext;
            _iwebhost = iwebhost;
        }

        [HttpPost]
        [Route("CreatePost")]
        public IActionResult CreatePost([FromForm] PostdataView formData)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            string imagePath = string.Empty;
            IFormFile file = formData.Image!;
            // getting file original name
            if (file.FileName != null)
            {
                string FileName = file.FileName;

                // combining GUID to create unique name before saving in wwwroot
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;

                //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/");
                var path = Path.Combine(_iwebhost.WebRootPath, "Upload");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // getting full path inside wwwroot/images
                 imagePath = Path.Combine(_iwebhost.WebRootPath, "Upload", uniqueFileName);

                // copying file
                file.CopyTo(new FileStream(imagePath, FileMode.Create));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "File is not exists!" });
            }

            var FilepathDb = Path.Combine("Upload",Path.GetFileName(imagePath));

            // Set initial values for CommentCount and LikeCount
            var post = new Post
            {
                Title = formData.Title,
                Description = formData.Description,
                CommentCount = 0,
                LikeCount = 0,
                Date = DateTime.Now,
                UserRefId = formData.logedInUserID,
                Image =FilepathDb,
            };

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            return Ok(post);
        }

        [HttpGet]
        [Route("viewpost")]
        public IActionResult ViewPost()
            {
            var post = _dbContext.Posts.Include(p => p.comments).OrderByDescending(p=>p.Date).ToList();
           // List<Post> post = _dbContext.Posts.ToList();
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpGet]
        [Route("mypost/{currentuserid}")]
        public IActionResult MyPostView([FromRoute] string currentuserid)
        {
            var post = _dbContext.Posts.Where(p => p.UserRefId == currentuserid).Include(p => p.comments).OrderByDescending(p => p.Date).ToList();
            // List<Post> post = _dbContext.Posts.ToList();
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        [Route("savecomment")]
        public IActionResult SaveComment([FromForm] ViewModelComment data)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            _dbContext.SaveChanges();
            var commentdata = new Comment
            {
                PostRefID = (int)data.PostRefID!,
                CommentText = data.CommentText,
                UserId=data.UserID,
                Username=data.UserName,
                Date = DateTime.Now,
            };

            _dbContext.Comments.Add(commentdata);
            _dbContext.SaveChanges();

            return Ok(commentdata);
        }

       
    }
}
