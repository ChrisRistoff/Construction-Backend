using construction.Dtos;

namespace construction.Interfaces;

public interface IAdminRepository
{
    Task<LoginResponseDto> LoginAdmin(LoginRequestDto user);
}
