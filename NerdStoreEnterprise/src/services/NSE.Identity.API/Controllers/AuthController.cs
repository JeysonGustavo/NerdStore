using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identity.API.Models;
using System.Threading.Tasks;

namespace NSE.Identity.API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("new-account")]
        public async Task<IActionResult> Register(UserRegister requestModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = requestModel.Email,
                Email = requestModel.Email,
                EmailConfirmed = true, // needs to send email for user to confirm
            };

            var result = await _userManager.CreateAsync(user, requestModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
                
            return BadRequest();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Login(UserLogin requestModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(requestModel.Email, requestModel.Password, false, true);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }
    }
}
