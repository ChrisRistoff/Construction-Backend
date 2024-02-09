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
}
