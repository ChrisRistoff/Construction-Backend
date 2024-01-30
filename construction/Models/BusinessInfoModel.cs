using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace construction.Models;

[Table("business_info")]
public class BusinessInfo
{
    [Column("info_id")]
    public int Id { get; set; }

    [Column("name")]
    [NotNull]
    public string? Name { get; set; }

    [Column("email")]
    [NotNull]
    public string? Email { get; set; }

    [Column("phone")]
    [NotNull]
    public string? Phone { get; set; }

    [Column("address")]
    [NotNull]
    public string? Address { get; set; }

    [Column("city")]
    [NotNull]
    public string? City { get; set; }

    [Column("info")]
    [NotNull]
    public string? Info { get; set; }

    [Column("logo")]
    [NotNull]
    public string? Logo { get; set; }

    [Column("facebook")]
    public string? Facebook { get; set; }

    [Column("instagram")]
    public string? Instagram { get; set; }

    [Column("youtube")]
    public string? Youtube { get; set; }

    [Column("tiktok")]
    public string? Tiktok { get; set; }

    [Column("linkedin")]
    public string? Linkedin { get; set; }
}
