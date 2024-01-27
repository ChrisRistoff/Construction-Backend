using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

[Table("jobs")]
public class Jobs
{
    [Column("job_id")]
    public int Id { get; set; }

    [Column("title")]
    [NotNull]
    public string? Title { get; set; }

    [Column("tagline")]
    [NotNull]
    public string? Tagline { get; set; }

    [Column("description")]
    [NotNull]
    public string? Description { get; set; }

    [Column("job_type")]
    [NotNull]
    public string? JobType { get; set; }

    [Column("date")]
    [NotNull]
    public DateTime? Date { get; set; }

    [Column("client")]
    [NotNull]
    public string? Client { get; set; }

    [Column("location")]
    [NotNull]
    public string? Location { get; set; }

    // job_type is a foreign key to JobTypes
    [ForeignKey("JobType")]
    public JobTypes? Name { get; set; }
}
