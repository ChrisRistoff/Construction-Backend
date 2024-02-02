using construction.Dtos;

namespace construction.Interfaces;

public interface IJobTypesRepository
{
    Task<IEnumerable<GetJobTypeDto>?> GetJobTypes();
    Task<GetJobTypeDto?> GetJobType(string name);
    // Task<GetJobTypesDto?> CreateJobType(CreateJobTypesDto jobType);
    // Task<GetJobTypesDto?> UpdateJobType(UpdateJobTypesDto jobType);
    // Task<bool?> DeleteJobType(int id);
}
