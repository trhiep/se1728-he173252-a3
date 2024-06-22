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
    public class DetailsModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public DetailsModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

      public SE1728_HE173252_A3.Models.Post Post { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(c => c.Category).FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            return Page();
        }
    }
}
