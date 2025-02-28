using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MsRazorMovie.Models;

namespace MsRazorMovie.Pages.Accounts
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("/Accounts/Login");
        }
    }
}
