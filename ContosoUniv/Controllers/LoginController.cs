using System.Security.Claims;
using ContosoUniv.Lib;
using ContosoUniv.Repositories;
using ContosoUniv.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniv.Controllers
{
    public class LoginController : Controller
    {
        
        private UserRepository UserRepository { get; }
        // GET: LoginController
        public LoginController(UserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }
        
        

        public ActionResult Login()
        {
            return View(new CredentialViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(CredentialViewModel credential)
        {
            if (ModelState.IsValid)
            {
                var user = await UserRepository.GetByEmail(credential.EmailAddress);
                if (user == null)
                {
                    return NotFound();
                }

                if (PasswordHasher.CheckPassword(credential.Password, user.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, credential.EmailAddress),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    };
	
                    var identity = new ClaimsIdentity(claims, "CookieScheme");
                    var userPrincipal = new ClaimsPrincipal(identity);
                    
                    await HttpContext.SignInAsync("CookieScheme", userPrincipal);
                }
               
            }
          
	
            return RedirectToAction("Index","Home");
	
        }
        
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("CookieScheme");
	
            return RedirectToAction("Index","Home");
	
	
        }

    }
    
    
}
