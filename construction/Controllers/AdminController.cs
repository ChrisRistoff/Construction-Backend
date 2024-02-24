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

            // check if username and password are empty
            if (string.IsNullOrEmpty(loginAdmin.Name) || string.IsNullOrEmpty(loginAdmin.Password))
            {
                return BadRequest("Username and password are required");
            }

            // check if username and password are correct
            LoginResponseDto loginResponse = await adminRepository.LoginAdmin(loginAdmin)!;

            // if username or password are incorrect
            if (loginResponse == null!)
            {
                return BadRequest("Username or password is incorrect");
            }

            // if username and password are correct
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

        // delay the response so I don't get a warning
        await Task.Delay(0);

        // return authorized message object
        return Ok(new {message = "Authorized"});
    }
}
