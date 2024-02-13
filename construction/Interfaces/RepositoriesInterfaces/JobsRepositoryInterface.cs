using construction.Dtos;

namespace construction.Interfaces;



public interface IJobsRepository
{

    // get all jobs
    Task<IEnumerable<GetAllJobsDto>?> GetJobs();

    // get job by id
    Task<GetJobDto?> GetJob(int id);

    // edit job
    Task<EditJobDto?> EditJob(EditJobDto job, int id);
}
