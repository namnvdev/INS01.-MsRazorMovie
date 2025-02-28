using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MsRazorMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace MsRazorMovie.Pages.Accounts
{
    public class RegisterModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty] public RegisterInputModel Input { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Successfully registered user: {Input.Email}");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("/Accounts/Index");
            }

            foreach (var error in result.Errors)
            {
                _logger.LogInformation($"Error: {error}");
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();

        }
    }

    public class RegisterInputModel
    {
        [Required] public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
    }
}
