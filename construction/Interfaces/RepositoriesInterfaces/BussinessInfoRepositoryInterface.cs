using construction.Dtos;

namespace construction.Interfaces;

public interface IBusinessInfoRepository
{
    Task<GetBusinessInfoDto?> GetBusinessInfo();
    Task<UpdateBusinessInfoDto?> UpdateBusinessInfo(UpdateBusinessInfoDto businessInfo);
}
