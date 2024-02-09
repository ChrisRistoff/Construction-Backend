using System.ComponentModel.DataAnnotations.Schema;

namespace construction.Dtos;

public class GetAllJobsDto
{
    public int Job_Id { get; set; }

    public string? Title { get; set; }

    public string? Tagline { get; set; }

    public string? Description { get; set; }

    public string? Job_Type { get; set; }

    public DateTime? Date { get; set; }

    public string? Client { get; set; }

    public string? Location { get; set; }
}

public class GetJobDto
{
    public int Job_Id { get; set; }

    public string? Title { get; set; }

    public string? Tagline { get; set; }

    public string? Description { get; set; }

    public string? Job_Type { get; set; }

    public DateTime? Date { get; set; }

    public string? Client { get; set; }

    public string? Location { get; set; }

    public List<Images>? Images { get; set; }
}

public class Images
{
    public int Image_Id { get; set; }

    public int Job_Id { get; set; }

    public string? Image { get; set; }
}
