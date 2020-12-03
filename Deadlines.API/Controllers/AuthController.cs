using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Deadlines.API.Entities;
using Deadlines.API.Services;
using Deadlines.API.Model;
using Deadlines.API.Helpers;
using Npgsql.Replication.TestDecoding;

namespace Deadlines.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly EfContext _context;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJwtTokenService _iJwtTokenService;

        public AccountController(EfContext context,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            IJwtTokenService iJwtTokenService)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _iJwtTokenService = iJwtTokenService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
                return BadRequest(new {
                    title = "Login failed",
                    invalidParams = new[] {
                        new {
                            name="email",
                            reason="Даний користувач не знайденний"
                        }
                    }
                });
            

            var result = _signInManager
                .PasswordSignInAsync(user, model.Password, false, false).Result;

            if (!result.Succeeded)
                return BadRequest(new {
                    title = "Validation errors",
                    invalidParams = new[] {
                        new {
                            name="password",
                            reason="Невірно введений пароль"
                        }
                    }
                });

            await _signInManager.SignInAsync(user, isPersistent: true);

            return Ok(
                new
                {
                    token = _iJwtTokenService.CreateToken(user)
                });
        }
    
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);

                var invalidParams = new List<object>();
                foreach (var error in errors)
                {
                    invalidParams.Add(new
                    {
                        name = error.Key,
                        reason = error.Value
                    });
                }
                
                return BadRequest(new
                {
                    title = "Validation errors",
                    invalidParams = invalidParams
                });
            }
            
            var user = new DbUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return Ok();
            
            return BadRequest(new { invalid = "Хюстон у нас проблеми!" });
        }
    }
}