using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.userName,
                Email = registerRequestDTO.userName
            };

            var identityResult =  await userManager.CreateAsync(identityUser,registerRequestDTO.password);

            if (identityResult.Succeeded)
            {
                // add roles to this user 
                if(registerRequestDTO.roles != null && registerRequestDTO.roles.Any()) 
                {
                  identityResult =   await userManager.AddToRolesAsync(identityUser, registerRequestDTO.roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registerd please login");
                    }

                }

            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)

        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.userName);

            if (user != null)
            {
               var checkPasswordResult = await userManager.CheckPasswordAsync(user,loginRequestDTO.password);

                if (checkPasswordResult)
                {
                    return Ok();
                }

            }
            return BadRequest("User or Password Incorrect");
        }

    }
}
