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
    public class MyPostsModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public MyPostsModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<SE1728_HE173252_A3.Models.Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Posts != null)
            {
                Post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.AuthorID == LogedInUser.UserID)
                .OrderByDescending(p => p.PostID)
                .ToListAsync();
            }
        }
    }
}
