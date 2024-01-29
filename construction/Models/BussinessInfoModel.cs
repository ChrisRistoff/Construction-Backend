using System.ComponentModel.DataAnnotations.Schema;

namespace construction.Models;

[Table("bussiness_info")]
public class BussinessInfo
{
    [Column("info_id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("city")]
    public string? City { get; set; }

    [Column("info")]
    public string? Info { get; set; }

    [Column("logo")]
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
