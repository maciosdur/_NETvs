using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;
using Microsoft.AspNetCore.Authorization;

namespace lab11.Pages.Articles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly lab11.Data.MyDbContext _context;

        public IndexModel(lab11.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
        }
    }
}
