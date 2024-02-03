using construction.Dtos;

namespace construction.Interfaces;



public interface IBusinessInfoRepository
{

    // get business info
    Task<GetBusinessInfoDto?> GetBusinessInfo();

    // update business info
    Task<UpdateBusinessInfoDto?> UpdateBusinessInfo(UpdateBusinessInfoDto businessInfo);
}
