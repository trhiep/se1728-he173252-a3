using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SE1728_HE173252_A3.Models;
using SE1728_HE173252_A3.Utils;

namespace SE1728_HE173252_A3.Pages.Post
{
    public class SearchModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public SearchModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchValue { get; set; }

        public IList<SE1728_HE173252_A3.Models.Post> Post { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                var posts = await query.ToListAsync();

                posts = posts.Where(p =>
                    p.Title.Contains(SearchValue) ||
                    p.PostID.ToString().Contains(SearchValue) ||
                    CustomConverter.ConvertHtmlToText(p.Content).Contains(SearchValue))
                    .OrderByDescending(p => p.PostID)
                    .ToList();

                Post = posts;
            }
            else
            {
                Post = await query
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.PostID)
                    .ToListAsync();
            }
        }
    }
}
