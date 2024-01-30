using System.Net;
using construction.Dtos;
using Newtonsoft.Json;

[Collection("Sequential")]
public class GetBusinessInfoTests
{
    [Fact]
    public async Task GetBusinessInfo()
    {
        var client = SharedTestResources.Factory.CreateClient();

        var response = await client.GetAsync("/construction/api/info");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();

        GetBussinessInfoDto? businessInfo = JsonConvert.DeserializeObject<GetBussinessInfoDto>(responseString);

        Assert.Equal("test", businessInfo!.Name);
        Assert.Equal("test", businessInfo!.Email);
        Assert.Equal("test", businessInfo!.Phone);
        Assert.Equal("test", businessInfo!.Address);
        Assert.Equal("test", businessInfo!.City);
        Assert.Equal("test", businessInfo!.Info);
        Assert.Equal("test", businessInfo!.Logo);
        Assert.Equal("test", businessInfo!.Facebook);
        Assert.Equal("test", businessInfo!.Instagram);
        Assert.Equal("test", businessInfo!.Youtube);
        Assert.Equal("test", businessInfo!.Tiktok);
        Assert.Equal("test", businessInfo!.Linkedin);
    }
}
