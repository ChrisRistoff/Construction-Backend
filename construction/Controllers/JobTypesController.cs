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

            // get job types
            var jobTypes = await jobTypesRepository.GetJobTypes();

            // return job types
            return Ok(jobTypes);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("construction/api/jobtypes/{name}")]
    public async Task<ActionResult<GetJobTypeDto>> GetJobType(string name)
    {
        try
        {

            // get job type by name
            var jobType = await jobTypesRepository.GetJobType(name);

            // check job type exists
            if (jobType == null)
            {
                return NotFound();
            }

            // return job type
            return Ok(jobType);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPost("construction/api/jobtypes")]
    [Authorize]
    public async Task<ActionResult<GetJobTypeDto>> CreateJobType(AddJobTypeDto jobType)
    {
        try
        {

            // create job type
            var newJobType = await jobTypesRepository.CreateJobType(jobType);

            // return job type
            return Ok(newJobType);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPatch("construction/api/jobtypes/{name}/image")]
    [Authorize]
    public async Task<ActionResult<GetJobTypeDto>> UpdateImageToJobType(string name, IFormFile image)
    {
        try
        {

            // update image to job type
            GetJobTypeDto? newJobType = await jobTypesRepository.UpdateImageToJobType(name, image);

            // return new job type
            return Ok(newJobType);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPatch("construction/api/jobtypes/{name}")]
    [Authorize]
    public async Task<ActionResult<GetJobTypeDto>> EditJobType(string name, EditJobTypeDto jobType)
    {
        try
        {

            // edit job type
            GetJobTypeDto? newJobType = await jobTypesRepository.EditJobType(name, jobType);

            if (newJobType == null)
            {
                return NotFound();
            }

            // return new job type
            return Ok(newJobType);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



}
