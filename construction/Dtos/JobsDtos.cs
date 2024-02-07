namespace construction.Dtos;

public class GetAllJobsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Tagline { get; set; }
    public string? Description { get; set; }
    public string? JobType { get; set; }
    public DateTime? Date { get; set; }
    public string? Client { get; set; }
    public string? Location { get; set; }
    public string? Name { get; set; }
}
