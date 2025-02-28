using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MsRazorMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace MsRazorMovie.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty] public LoginInputModel Input { get; set; }

        /// <summary>
        /// Using Dependencies Injections to get several objects from Identity System and Logger
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        public LoginModel(SignInManager<ApplicationUser> signInManager, 
                    UserManager<ApplicationUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// This function is called when user submit from the UI page (Post method)
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is invalid");
                return Page();
            }
            var inputUser = _userManager.NormalizeEmail(Input.Email);

            var user = await _userManager.FindByNameAsync(inputUser);
            if (user == null)
            {
                _logger.LogInformation($"{inputUser} not found");
                return Page();
            }
            _logger.LogInformation($"{user.NormalizedUserName} logged in");

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, Input.Password);
            if (!isPasswordValid)
            {
                _logger.LogInformation($"{Input.Password} is invalid");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(inputUser, Input.Password, false, false);
            if (result.Succeeded)
                return RedirectToPage("/Accounts/Index");
            return Page();

        }

        public class LoginInputModel
        {
            [Required] public string Email { get; set; }
            [Required][DataType(DataType.Password)] public string Password { get; set; }
        }
    }
}
