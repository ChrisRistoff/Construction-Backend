using construction.Dtos;

namespace construction.Seeds.Data;

public class JobsData
{
    public GetAllJobsDto[] GetJobsData()
    {
        return new GetAllJobsDto[]
        {
            new GetAllJobsDto
            {
                Id = 1,
                Title = "test",
                Tagline = "test",
                Description = "test",
                JobType = "test",
                Date = new DateTime(2022, 1, 1),
                Client = "test",
                Location = "test",
                Name = "test"
            },

            new GetAllJobsDto
            {
                Id = 2,
                Title = "test",
                Tagline = "test",
                Description = "test",
                JobType = "test",
                Date = new DateTime(2022, 1, 1),
                Client = "test",
                Location = "test",
                Name = "test"
            }
        };
    }
}
