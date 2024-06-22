using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;

namespace SE1728_HE173252_A3.Pages.Post
{
    public class IndexModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public IndexModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<SE1728_HE173252_A3.Models.Post> Post { get; set; } = default!;
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageNumber)
        {
            PageNumber = pageNumber ?? 1;
            int totalRecords = await _context.Posts.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            if (_context.Posts != null)
            {
                Post = await _context.Posts
                    .OrderByDescending(p => p.PostID)
                    .Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .ToListAsync();
            }
        }
    }
}
