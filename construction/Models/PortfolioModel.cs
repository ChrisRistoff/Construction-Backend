using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

[Table("portfolio")]
public class Portfolio
{
    [Column("id")]
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

    [Column("image")]
    [NotNull]
    public string? Image { get; set; }

    [Column("job_type")]
    [NotNull]
    public string? JobType { get; set; }

    [Column("date")]
    [NotNull]
    public DateTime Date { get; set; }

    [Column("client")]
    [NotNull]
    public string? Client { get; set; }

    [Column("location")]
    [NotNull]
    public string? Location { get; set; }

    [ForeignKey("JobType")]
    public JobTypes? Name { get; set; }
}
