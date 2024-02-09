using construction.Dtos;

namespace construction.Interfaces;



public interface IJobsRepository
{

    // get all jobs
    Task<IEnumerable<GetAllJobsDto>?> GetJobs();

}
