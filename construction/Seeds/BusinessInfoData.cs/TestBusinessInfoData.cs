using construction.Dtos;

namespace construction.Seed;

public class BusinessInfoData
{
    public GetBusinessInfoDto GetBusinessInfoData()
    {
        return new GetBusinessInfoDto
        {
            Info_id = 1,
            Name = "test",
            Email = "test",
            Phone = "test",
            Address = "test",
            City = "test",
            Info = "test",
            Logo = "test",
            Facebook = "test",
            Instagram = "test",
            Youtube = "test",
            Tiktok = "test",
            Linkedin = "test"
        };
    }
}
