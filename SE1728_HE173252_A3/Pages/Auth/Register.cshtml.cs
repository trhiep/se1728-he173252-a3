using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_HE173252_A3.Models;

namespace SE1728_HE173252_A3.Pages.Auth
{
    public class RegisterModel : BasePageModel
    {
        private readonly SE1728_HE173252_A3.Models.ApplicationDBContext _context;

        public RegisterModel(SE1728_HE173252_A3.Models.ApplicationDBContext context)
        {
            _context = context;
        }

        public string ErrorMessage;
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
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (AppUser != null)
            {
                AppUser appUser = _context.AppUsers.Where(user => user.Email == AppUser.Email).FirstOrDefault();
                if (appUser == null)
                {
                    _context.AppUsers.Add(AppUser);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Created new account successfully!";
                    return RedirectToPage("/Auth/Login");
                }
                else
                {
                    ErrorMessage = "The email " + AppUser.Email + " is already exists! Please choose another";
                }
            }
            return Page(); 
        }
    }
}
