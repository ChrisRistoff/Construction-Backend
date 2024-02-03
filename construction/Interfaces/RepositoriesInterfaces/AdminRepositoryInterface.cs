using construction.Dtos;

namespace construction.Interfaces;



public interface IAdminRepository
{

    // login admin
    Task<LoginResponseDto> LoginAdmin(LoginRequestDto user);
}
