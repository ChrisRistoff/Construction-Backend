using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

[Table("booking_requests")]
public class BookingRequest
{
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("start_time")]
    public TimeSpan StartTime { get; set; }

    [Column("end_time")]
    public TimeSpan EndTime { get; set; }

    [Column("name")]
    [NotNull]
    public string? Name { get; set; }

    [Column("email")]
    [NotNull]
    public string? Email { get; set; }
}
