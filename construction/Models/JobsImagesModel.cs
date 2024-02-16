using System.ComponentModel.DataAnnotations.Schema;

namespace construction.Models;

[Table("jobs_images")]
public class JobsImages
{

    [Column("image_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("job_id")]
    public int JobId { get; set; }

    [Column("image")]
    public string? Image { get; set; }

    // job_id is a foreign key to Jobs
    [ForeignKey("job_id")]
    public Jobs? Job { get; set; }
}
