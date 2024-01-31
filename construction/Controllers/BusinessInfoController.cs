using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class PersonalInfoController(BusinessInfoRepository businessInfoRepository) : ControllerBase
{
    [HttpGet("construction/api/info")]
    public async Task<ActionResult<GetBusinessInfoDto>> GetPersonalInfo()
    {
        var personalInfo = await businessInfoRepository.GetBusinessInfo();
        return Ok(personalInfo);
    }
}
