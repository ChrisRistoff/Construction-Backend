using construction.Dtos;
using construction.Models;

namespace portfolio.Interfaces;

public interface IAdminRepository
{
    Task<LoginResponseDto> LoginAdmin(LoginRequestDto user);
}
