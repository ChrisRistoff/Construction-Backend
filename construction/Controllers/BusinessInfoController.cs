using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class PersonalInfoController(BusinessInfoRepository businessInfoRepository) : ControllerBase
{
    [HttpGet("construction/api/info")]
    public async Task<ActionResult<GetBussinessInfoDto>> GetPersonalInfo()
    {
        var personalInfo = await businessInfoRepository.GetBusinessInfo();
        return Ok(personalInfo);
    }
}
