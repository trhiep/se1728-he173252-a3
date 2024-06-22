using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using SE1728_HE173252_A3.Models;
using SE1728_HE173252_A3.Utils;

namespace SE1728_HE173252_A3.Pages.Post
{
    public class IndexModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public IndexModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
            int totalRecords = _context.Posts.Count();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
        }

        public IList<SE1728_HE173252_A3.Models.Post> Post { get; set; } = default!;
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

        public ContentResult OnGetGetPosts(int? pageNumber)
        {
            PageNumber = pageNumber ?? 1;
            var posts = _context.Posts
                    .OrderByDescending(p => p.PostID)
                    .Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .ToList();

            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var item in posts)
            {
                postsDTO.Add(new PostDTO
                {
                    Title = item.Title,
                    Author = item.Author.Fullname,
                    Content = item.Content,
                    Category = item.Category.CategoryName,
                    CreatedDate = CustomConverter.GetFormatedDateTime(item.CreatedDate),
                    UpdatedDate = CustomConverter.GetFormatedDateTime(item.UpdatedDate)
                });
            }
            string jsonStr = JsonSerializer.Serialize(postsDTO);
            return Content(jsonStr);
        }
    }

    public class PostDTO
    {
        public string Title { get; set; } = "N/A";
        public string Author { get; set; } = "N/A";
        public string Category { get; set; } = "N/A";
        public string Content { get; set; } = "N/A";
        public string CreatedDate { get; set; } = "N/A";
        public string? UpdatedDate { get; set; }

    }
}
