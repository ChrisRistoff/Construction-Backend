using System.ComponentModel.DataAnnotations.Schema;

namespace construction.Models;

[Table("admin")]
public class Admin
{
    [Column("admin_id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("role")]
    public string Role { get; set; }
}
