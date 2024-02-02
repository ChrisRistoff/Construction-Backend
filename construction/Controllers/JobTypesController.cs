using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using construction.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace construction.Controllers;

[ApiController]
public class JobTypesController(JobTypesRepository jobTypesRepository) : ControllerBase
{
    [HttpGet("construction/api/jobtypes")]
    public async Task<ActionResult<IEnumerable<GetJobTypeDto>>> GetJobTypes()
    {
        try
        {
            var jobTypes = await jobTypesRepository.GetJobTypes();
            return Ok(jobTypes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("construction/api/jobtypes/{id}")]
    public async Task<ActionResult<GetJobTypeDto>> GetJobType(int id)
    {
        try
        {
            var jobType = await jobTypesRepository.GetJobType(id);

            if (jobType == null)
            {
                return NotFound();
            }

            return Ok(jobType);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
