using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE1728_HE173252_A3.Models;
using System.Text.Json;

namespace SE1728_HE173252_A3.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public AppUser App { get; set; }

        public void OnGet()
        {
            
        }
    }
}
