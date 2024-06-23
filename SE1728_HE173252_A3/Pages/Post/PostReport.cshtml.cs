using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class PostReportModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public PostReportModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string EndDate { get; set; }

        public string StartDateDisplay { get; set; }
        public string EndDateDisplay { get; set; }

        public IList<SE1728_HE173252_A3.Models.Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DateTime endDateVal = CustomConverter.GetFormatedDateTimeFromString(EndDate);
            if (endDateVal == default(DateTime))
            {
                endDateVal = DateTime.Today;
            }

            DateTime startDateVal = CustomConverter.GetFormatedDateTimeFromString(StartDate);
            if (startDateVal == default(DateTime))
            {
                startDateVal = endDateVal.AddMonths(-1);
            }

            if (_context.Posts != null)
            {
                Post = await _context.Posts
                .Where(o => o.CreatedDate.Date >= startDateVal.Date && o.CreatedDate.Date <= endDateVal.Date)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .OrderByDescending(o => o.PostID)
                .ToListAsync();
            }

            StartDateDisplay = startDateVal.ToString("yyyy/MM/dd");
            EndDateDisplay = endDateVal.ToString("yyyy/MM/dd");
        }

        public ContentResult OnGetGetPostsByDate(string startDate, string endDate)
        {
            DateTime endDateVal = CustomConverter.GetFormatedDateTimeFromStringFromFetch(endDate);
            if (endDateVal == default(DateTime))
            {
                endDateVal = DateTime.Today;
            }

            DateTime startDateVal = CustomConverter.GetFormatedDateTimeFromStringFromFetch(startDate);
            if (startDateVal == default(DateTime))
            {
                startDateVal = endDateVal.AddMonths(-1);
            }

            if (_context.Posts != null)
            {
                Post = _context.Posts
                .Where(o => o.CreatedDate.Date >= startDateVal.Date && o.CreatedDate.Date <= endDateVal.Date)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .OrderByDescending(o => o.PostID)
                .ToList();
            }

            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var item in Post)
            {
                postsDTO.Add(new PostDTO
                {
                    PostID = item.PostID.ToString(),
                    Title = item.Title,
                    Author = item.Author.Email,
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
