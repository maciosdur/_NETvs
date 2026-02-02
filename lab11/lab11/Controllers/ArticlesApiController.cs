using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;

namespace lab11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ArticlesApiController(MyDbContext context) => _context = context;

        // GET: api/ArticlesApi?skip=0&take=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(int skip = 0, int take = 3, int? categoryId = null)
        {
            var query = _context.Articles.Include(a => a.Category).AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }

            return await query
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        // GET: api/ArticlesApi/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // POST: api/ArticlesApi
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle([FromBody] Article article)
        {
            article.Id = 0;

            article.Category = null;

            _context.Articles.Add(article); 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article);
        }

        // PUT: api/ArticlesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, [FromBody] Article article) // <--- KROK 1: Dodaj [FromBody]
        {
            if (id != article.Id)
            {
                return BadRequest($"ID mismatch: URL({id}) != Body({article.Id})");
            }

            article.Category = null;

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Articles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ArticlesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}