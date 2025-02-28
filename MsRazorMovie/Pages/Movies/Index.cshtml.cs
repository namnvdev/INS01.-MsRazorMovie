using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MsRazorMovie.Data;
using MsRazorMovie.Models;

namespace MsRazorMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MsRazorMovie.Data.ApplicationDbContext _context;

        public IndexModel(MsRazorMovie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.ToListAsync();
        }
    }
}
