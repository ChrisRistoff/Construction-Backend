using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using construction.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace construction.Controllers;



[ApiController]
public class JobsController(JobsRepository jobsRepository) : ControllerBase
{



    [HttpGet("construction/api/jobs")]
    public async Task<ActionResult<IEnumerable<GetAllJobsDto>>> GetJobs()
    {
        try
        {

            // get jobs
            var jobs = await jobsRepository.GetJobs();

            // return jobs
            return Ok(jobs);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpGet("construction/api/jobs/{id}")]
    public async Task<ActionResult<GetJobDto>> GetJob(int id)
    {
        try
        {

            // get job
            var job = await jobsRepository.GetJob(id);

            // 404 if job not found
            if (job == null)
            {
                return NotFound();
            }

            // return job
            return Ok(job);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPatch("construction/api/jobs/{id}")]
    [Authorize]
    public async Task<ActionResult<EditJobDto>> EditJob(EditJobDto job, int id)
    {
        try
        {
            // edit job
            var editedJob = await jobsRepository.EditJob(job, id);

            // 404 if job not found
            if (editedJob == null)
            {
                return NotFound();
            }

            // return job
            return Ok(editedJob);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
