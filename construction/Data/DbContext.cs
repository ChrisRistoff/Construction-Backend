using construction.Models;
using Microsoft.EntityFrameworkCore;

namespace construction.Data;



public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    // business_info table
    public DbSet<BusinessInfo> BusinessInfo { get; set; } = null!;

    // booking_request table
    public DbSet<BookingRequest> BookingRequests { get; set; } = null!;

    // job_types table
    public DbSet<JobTypes> JobTypes { get; set; } = null!;

    // jobs table
    public DbSet<Jobs> Jobs { get; set; } = null!;

    // jobs_images table
    public DbSet<Admin> Admins { get; set; } = null!;

    // jobs_images table
    public DbSet<JobsImages> JobsImages { get; set; } = null!;
}
