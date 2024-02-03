using construction.Dtos;

namespace construction.Interfaces;



public interface IJobTypesRepository
{

    // get job types
    Task<IEnumerable<GetJobTypeDto>?> GetJobTypes();

    // get job type by name
    Task<GetJobTypeDto?> GetJobType(string name);


    // these are not used yet so I commented them out

    // Task<GetJobTypesDto?> CreateJobType(CreateJobTypesDto jobType);
    // Task<GetJobTypesDto?> UpdateJobType(UpdateJobTypesDto jobType);
    // Task<bool?> DeleteJobType(int id);
}
