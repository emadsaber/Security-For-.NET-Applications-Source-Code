using CookieAuthentication.App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CookieAuthentication.App.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Input { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Verify the credential (should be done from database)
            // For demo purpose, we are using hard-coded values
            if (Input.UserName == "admin" && Input.Password == "password")
            {
                // Creating the security context
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@expertdevs.com")
                };
                var identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookie", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class Credential
    {
        [Required]        
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
