namespace construction.Dtos;

public class LoginResponseDto
{
    public string? Username { get; set; }
    public string? Token { get; set; }
    public string? Role { get; set; }
}

public class LoginRequestDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}
