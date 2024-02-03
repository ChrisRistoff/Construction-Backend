using System.Net;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class GetBusinessInfoTests
{



    [Fact]
    public async Task GetBusinessInfo()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the business info
        var response = await client.GetAsync("/construction/api/info");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        GetBusinessInfoDto? businessInfo = JsonConvert.DeserializeObject<GetBusinessInfoDto>(responseString);

        // check if the business info is correct
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
