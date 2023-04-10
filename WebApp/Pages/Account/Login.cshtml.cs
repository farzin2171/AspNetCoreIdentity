using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        
        [BindProperty]
        public Credential credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if(!ModelState.IsValid)
            {
                return Page();
            }

            //verify the credential
            if(credential.UserName == "admin" && credential.Password == "password")
            {
                //create the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , "admin"),
                    new Claim(ClaimTypes.Email , "admin@gmail.com"),
                    new Claim("Department" , "HR"),
                    new Claim("Admin","true"),
                    new Claim("EmploymentDate","2021-05-01"),
                    new Claim("Manager","True")


                };
                var identity = new ClaimsIdentity(claims , "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                //1- serialize principle
                //2- encrypt the serilized principle
                //3- save principle in the cookie
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();

        }
    }

    public class Credential
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
