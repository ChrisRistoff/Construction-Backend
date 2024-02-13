using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using construction.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace construction.Controllers;



[ApiController]
public class BusinessInfoController(BusinessInfoRepository businessInfoRepository) : ControllerBase
{



    [HttpGet("construction/api/info")]
    public async Task<ActionResult<GetBusinessInfoDto>> GetPersonalInfo()
    {
        try
        {

            // get personal info
            var businessInfo = await businessInfoRepository.GetBusinessInfo();

            // return personal info
            return Ok(businessInfo);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPatch("construction/api/info")]
    [Authorize]
    public async Task<ActionResult<UpdateBusinessInfoDto>> UpdatePersonalInfo(UpdateBusinessInfoDto businessInfo)
    {
        try
        {

            // update personal info
            var updatedBusinessInfo = await businessInfoRepository.UpdateBusinessInfo(businessInfo);

            // return updated personal info
            return Ok(updatedBusinessInfo);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
