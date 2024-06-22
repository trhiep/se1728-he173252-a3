using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;
using SE1728_HE173252_A3.Utils;

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

        public ContentResult OnGetGetMyPosts()
        {
            Post = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.AuthorID == LogedInUser.UserID)
                .OrderByDescending(p => p.PostID)
                .ToList();

            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var item in Post)
            {
                postsDTO.Add(new PostDTO
                {
                    PostID = item.PostID.ToString(),
                    Title = item.Title,
                    Author = item.Author.Fullname,
                    Content = item.Content,
                    Category = item.Category.CategoryName,
                    CreatedDate = CustomConverter.GetFormatedDateTime(item.CreatedDate),
                    UpdatedDate = item.CreatedDate == item.UpdatedDate ? "Not edited" : CustomConverter.GetFormatedDateTime(item.UpdatedDate)
                });
            }
            string jsonStr = JsonSerializer.Serialize(postsDTO);
            return Content(jsonStr);
        }
    }
}
