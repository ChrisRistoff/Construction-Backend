using construction.Dtos;
using construction.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace construction.Controllers;

[ApiController]
public class AdminController(AdminRepository adminRepository) : ControllerBase
{

    [HttpPost("construction/api/login-admin")]
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

            LoginResponseDto loginResponse = await adminRepository.LoginAdmin(loginAdmin)!;

            if (loginResponse == null!)
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

    [HttpGet("construction/api/test-auth")]
    [Authorize]
    public async Task<IActionResult> TestAuth()
    {
        await Task.Delay(0);
        return Ok("You are authorized");
    }
}
