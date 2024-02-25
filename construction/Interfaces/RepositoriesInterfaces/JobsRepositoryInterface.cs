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

    // add job
    Task<GetJobDto?> AddJob(AddJobDto job);

    // delete job
    Task<GetJobDto?> DeleteJob(int id);

    // add image to job
    Task<GetJobImageDto?> AddImageToJob(int id, IFormFile image);

    // delete image from job
    Task<GetJobImageDto?> DeleteImageFromJob(int imageId, string? image);
}
