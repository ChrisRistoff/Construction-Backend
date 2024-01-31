namespace construction.Dtos;

public class GetBusinessInfoDto
{
    public int Info_id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Info { get; set; }
    public string? Logo { get; set; }
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? Youtube { get; set; }
    public string? Tiktok { get; set; }
    public string? Linkedin { get; set; }
}

public class UpdateBusinessInfoDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Info { get; set; }
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? Youtube { get; set; }
    public string? Tiktok { get; set; }
    public string? Linkedin { get; set; }
}
