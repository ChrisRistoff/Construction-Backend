using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

[Table("admin")]
public class Admin
{
    [Column("admin_id")]
    public int Id { get; set; }

    [Column("name")]
    [NotNull]
    public string? Name { get; set; }

    [Column("password")]
    [NotNull]
    public string? Password { get; set; }

    [Column("role")]
    [NotNull]
    public string? Role { get; set; }
}
