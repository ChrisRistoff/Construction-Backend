using construction.Models;

namespace construction.Seed;

public class AdminData
{
    public Admin[] GetAdminData()
    {
        return new Admin[]
        {
            new Admin
            {
                Id = 1,
                Name = "test",
                Password = "test",
                Role = "test"
            }
        };
    }
}
