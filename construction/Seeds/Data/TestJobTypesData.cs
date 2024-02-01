using construction.Dtos;

namespace construction.Seed;

public class JobTypesData
{
    public IEnumerable<GetJobTypesDto> GetJobTypesData()
    {
        return new List<GetJobTypesDto>
        {
            new GetJobTypesDto
            {
                Name = "test",
                Description = "test",
                Image = "test",
                Icon = "test"
            },

            new GetJobTypesDto
            {
                Name = "test2",
                Description = "test2",
                Image = "test2",
                Icon = "test2"
            },

            new GetJobTypesDto
            {
                Name = "test3",
                Description = "test3",
                Image = "test3",
                Icon = "test3"
            }
        };
    }
}
