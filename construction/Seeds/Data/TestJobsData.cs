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
                Job_Id = 1,
                Title = "test",
                Tagline = "test",
                Description = "test",
                Job_Type = "test",
                Date = new DateTime(2022, 1, 1),
                Client = "test",
                Location = "test",
            },

            new GetAllJobsDto
            {
                Job_Id = 2,
                Title = "test",
                Tagline = "test",
                Description = "test",
                Job_Type = "test",
                Date = new DateTime(2022, 1, 1),
                Client = "test",
                Location = "test",
            }
        };
    }
}
