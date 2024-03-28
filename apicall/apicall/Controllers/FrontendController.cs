using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace apicall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrontendController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FrontendController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var postsTask = GetPostsAsync();
            var commentsTask = GetCommentsAsync();

            await Task.WhenAll(postsTask, commentsTask);

            var postsResult = await postsTask;
            var commentsResult = await commentsTask;

            var top5Posts = postsResult.Take(5);
            var top5Comments = commentsResult.Take(5);

            return Ok(new { Posts = top5Posts, Comments = top5Comments });
        }

        private async Task<List<Post>> GetPostsAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            var jsonString = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(jsonString);
            return posts;
        }

        private async Task<List<Comment>> GetCommentsAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments");
            var jsonString = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(jsonString);
            return comments;
        }
    }

    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class Comment
    {
        public int PostId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
