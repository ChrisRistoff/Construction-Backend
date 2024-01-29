using construction.Models;

namespace construction.Seed;

public class TestAdminData
{
    public Admin[] GetTestAdminData()
    {
        return new Admin[]
        {
            new Admin
            {
                Name = "test",
                Password = "test",
                Role = "test"
            }
        };
    }
}
