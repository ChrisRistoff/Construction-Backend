using construction.Dtos;



namespace construction.Seeds;

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
                Date = new DateTime(2022, 1, 1),
                Job_Type = "test",
                Client = "test",
                Location = "test",
            },

            new GetAllJobsDto
            {
                Job_Id = 2,
                Title = "test",
                Tagline = "test",
                Description = "test",
                Date = new DateTime(2022, 1, 1),
                Job_Type = "test",
                Client = "test",
                Location = "test",
            }
        };
    }
}
