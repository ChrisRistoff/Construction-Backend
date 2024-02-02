using construction.Dtos;

namespace construction.Seed;

public class JobTypesData
{
    public IEnumerable<GetJobTypeDto> GetJobTypesData()
    {
        return new List<GetJobTypeDto>
        {
            new GetJobTypeDto
            {
                Name = "test",
                Description = "test",
                Image = "test",
                Icon = "test"
            },

            new GetJobTypeDto
            {
                Name = "test2",
                Description = "test2",
                Image = "test2",
                Icon = "test2"
            },

            new GetJobTypeDto
            {
                Name = "test3",
                Description = "test3",
                Image = "test3",
                Icon = "test3"
            }
        };
    }
}
