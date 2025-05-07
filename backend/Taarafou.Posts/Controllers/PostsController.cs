using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Taarafou.Posts;  // مساحة الاسم الخاصة بـ PostsContext و Post

namespace Taarafou.Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsContext _context;
        public PostsController(PostsContext context)
            => _context = context;

        // GET api/posts?page=1&pageSize=5&search=...
        [HttpGet]
        public async Task<IActionResult> Get(
            int page = 1,
            int pageSize = 5,
            string? search = null
        )
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 5;

            var query = _context.Posts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Title.Contains(search));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new {
                items,
                page,
                pageSize,
                totalCount,
                totalPages
            });
        }

        // GET api/posts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        // POST api/posts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            post.CreatedAt = post.UpdatedAt = System.DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        // PUT api/posts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post updated)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            post.Title     = updated.Title;
            post.Body      = updated.Body;
            post.UpdatedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
