
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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
               bool checkPasswordResult = await userManager.CheckPasswordAsync(user,loginRequestDTO.password);

                if (checkPasswordResult)
                {
                    //Get Roles for this user 
                   var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        // create JWT Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                         JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }

            }
            return BadRequest("User or Password Incorrect");
        }

    }
}
