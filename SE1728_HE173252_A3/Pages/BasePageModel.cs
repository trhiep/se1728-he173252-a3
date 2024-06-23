using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_HE173252_A3.Models;
using System.Text.Json;

namespace SE1728_HE173252_A3.Pages
{
    public class BasePageModel : PageModel
    {

        Dictionary<string, bool> UserAccessMap = new Dictionary<string, bool>()
        {
            {"/Index", false },
            {"/Auth/Login", false },
            {"/Auth/Register", false },
            {"/Auth/Logout", false },
            {"/Post/Index", true },
            {"/Post/Create", true },
            {"/Post/Details", true },
            {"/Post/Delete", true },
            {"/Post/Edit", true },
            {"/Post/MyPosts", true },
            {"/Post/Search", true },
            {"/Post/PageDetailsModal", true },
            {"/Post/PostReport", true },
        };

        public AppUser LogedInUser { get; private set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            var AppUserJson = HttpContext.Session.GetString("AppUser");

            if (AppUserJson != null)
            {
                LogedInUser = JsonSerializer.Deserialize<AppUser>(AppUserJson);
            }


            // Check login required
            var accessingPage = context.ActionDescriptor.DisplayName;
            if (accessingPage != null)
            {
                bool isAuthenticatedRequired = UserAccessMap[accessingPage];
                if (isAuthenticatedRequired)
                {
                    if (LogedInUser == null)
                    {
                        context.Result = new RedirectToPageResult("/Auth/Login");
                    }
                }
            }
       
        }

    }
}
