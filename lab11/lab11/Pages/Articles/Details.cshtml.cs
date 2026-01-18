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
    public class DetailsModel : PageModel
    {
        private readonly lab11.Data.MyDbContext _context;

        public DetailsModel(lab11.Data.MyDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            Article = article;
            return Page();
        }
    }
}
