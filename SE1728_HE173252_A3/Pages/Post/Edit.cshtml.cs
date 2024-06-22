using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;
using SE1728_SignalR_CRUD;

namespace SE1728_HE173252_A3.Pages.Post
{
    public class EditModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;
        private readonly IHubContext<SignalRHub> _hubContext;

        public EditModel(SE1728_HE173252_A3.Models.ApplicationDBContext context, IHubContext<SignalRHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public SE1728_HE173252_A3.Models.Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post;
           ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "UserID");
           ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Post.UpdatedDate = DateTime.Now;
            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.PostID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _hubContext.Clients.All.SendAsync("LoadPosts");
            await _hubContext.Clients.All.SendAsync("LoadMyPosts");
            return RedirectToPage("./MyPosts");
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
