using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_HE173252_A3.Models;

namespace SE1728_HE173252_A3.Pages.Post
{
    public class CreateModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public CreateModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName");
            return Page();
        }

        [BindProperty]
        public SE1728_HE173252_A3.Models.Post Post { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            Post.AuthorID = LogedInUser.UserID;
            Post.CreatedDate = DateTime.Now;
            Post.UpdatedDate = Post.CreatedDate;
            Post.PublishStatus = 1;
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
