using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

public class JobTypes
{
    [Column("name")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [NotNull]
    public string? Name { get; set; }

    [Column("description")]
    [NotNull]
    public string? Description { get; set; }

    [Column("image")]
    [NotNull]
    public string? Image { get; set; }

    [Column("icon")]
    [NotNull]
    public string? Icon { get; set; }
}
