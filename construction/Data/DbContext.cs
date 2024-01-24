using construction.Models;
using Microsoft.EntityFrameworkCore;

namespace construction.Data;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    public DbSet<BookingRequest> BookingRequests { get; set; } = null!;
}

