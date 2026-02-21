using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactCRUDSupport_v1.Models.DTOs.User;
using ReactCRUDSupport_v1.Services;

namespace ReactCRUDSupport_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;
        public UserController(UserManager<IdentityUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }


        //api/User/Register
        //method: POST
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            RegisterResponseDto result = new RegisterResponseDto();

            IdentityUser identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,

            };
            var response = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (response.Succeeded)
            {
                response = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (response.Succeeded)
                {
                    result.Result = true;
                    result.Message = "User successfully created";
                    return Ok(result);
                }
            }
            result.Result = false;
            result.Message = "Password doesnt match requirements, or there could be some API side error";
            return BadRequest(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            LoginResponseDto response = new();
            var user = await _userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null)
            {
                var passwordValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (passwordValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        string token = _authService.CreateJWTToken(user, roles.ToList());
                        response.JwtToken = token;
                        response.Result = true;
                        response.Message = "Login successful!";

                        return Ok(response);
                    }
                }
            }
            response.Result = false;
            response.Message = "Invalid username or password";
            
            return BadRequest(response);
        }
    }
}
