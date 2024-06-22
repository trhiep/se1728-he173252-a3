using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;
using System.Text.Json;

namespace SE1728_HE173252_A3.Pages.Auth
{
    public class LoginModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public LoginModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (LogedInUser != null)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }

        [BindProperty]
        public AppUser AppUser { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.AppUsers.Where(u => u.Email == AppUser.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                if (AppUser.Password == user.Password)
                {
                    var userJson = JsonSerializer.Serialize(user);
                    HttpContext.Session.SetString("AppUser", userJson);
                    return RedirectToPage("/Post/Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Wrong password!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "This account does not exists!";
            }
            return Page();
            
        }
    }
}
