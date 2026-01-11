using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab11.Data;
using lab11.Models;

namespace lab11.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly lab11.Data.MyDbContext _context;

        public IndexModel(lab11.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
