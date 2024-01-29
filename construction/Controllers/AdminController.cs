using construction.Dtos;
using construction.Repositories;
using construction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace portfolio.Controllers;

[ApiController]
public class AdminController(AdminRepository adminRepository, AuthService authService) : ControllerBase
{

    [HttpPost("api/login-admin")]
    public async Task<ActionResult<LoginResponseDto>> LoginAdmin(LoginRequestDto loginAdmin)
    {
        try
        {
            if (string.IsNullOrEmpty(loginAdmin.Name))
            {
                return BadRequest("Username is required");
            }

            if (string.IsNullOrEmpty(loginAdmin.Password))
            {
                return BadRequest("Password is required");
            }

            LoginResponseDto loginResponse = await adminRepository.LoginAdmin(loginAdmin);

            if (loginResponse.Name == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(loginResponse);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/test-auth")]
    [Authorize]
    public async Task<IActionResult> TestAuth()
    {
        await Task.Delay(0);
        return Ok("You are authorized");
    }
}
