using construction.Models;
using Microsoft.EntityFrameworkCore;

namespace construction.Data;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    public DbSet<BusinessInfo> BusinessInfo { get; set; } = null!;
    public DbSet<BookingRequest> BookingRequests { get; set; } = null!;
    public DbSet<JobTypes> JobTypes { get; set; } = null!;
    public DbSet<Jobs> Jobs { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<JobsImages> JobsImages { get; set; } = null!;
}
