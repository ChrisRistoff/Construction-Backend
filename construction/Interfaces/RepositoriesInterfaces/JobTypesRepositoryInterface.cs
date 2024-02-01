using construction.Dtos;

namespace construction.Interfaces;

public interface IJobTypesRepository
{
    Task<IEnumerable<GetJobTypesDto>?> GetJobTypes();
    // Task<GetJobTypesDto?> GetJobType(int id);
    // Task<GetJobTypesDto?> CreateJobType(CreateJobTypesDto jobType);
    // Task<GetJobTypesDto?> UpdateJobType(UpdateJobTypesDto jobType);
    // Task<bool?> DeleteJobType(int id);
}
