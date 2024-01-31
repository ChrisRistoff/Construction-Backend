using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using construction.Repositories;

namespace construction.Controllers;

[ApiController]
public class PersonalInfoController(BusinessInfoRepository businessInfoRepository) : ControllerBase
{
    [HttpGet("construction/api/info")]
    public async Task<ActionResult<GetBusinessInfoDto>> GetPersonalInfo()
    {
        try
        {
            var businessInfo = await businessInfoRepository.GetBusinessInfo();
            return Ok(businessInfo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("construction/api/info")]
    public async Task<ActionResult<UpdateBusinessInfoDto>> UpdatePersonalInfo(UpdateBusinessInfoDto businessInfo)
    {
        try
        {
            var updatedBusinessInfo = await businessInfoRepository.UpdateBusinessInfo(businessInfo);

            return Ok(updatedBusinessInfo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
