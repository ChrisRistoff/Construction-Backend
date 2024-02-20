using construction.Dtos;

namespace construction.Interfaces;



public interface IJobTypesRepository
{

    // get job types
    Task<IEnumerable<GetJobTypeDto>?> GetJobTypes();

    // get job type by name
    Task<GetJobTypeDto?> GetJobType(string name);

    // create job type
    Task<AddJobTypeDto?> CreateJobType(AddJobTypeDto jobType);

    // edit job type
    Task<GetJobTypeDto?> EditJobType(string name, EditJobTypeDto jobType);

    // delete job type
    Task<bool?> DeleteJobType(int id);
}
