using System.Net;
using System.Net.Http.Headers;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;

[Collection("Sequential")]
public class UpdateBusinessInfoTests
{
    [Fact]
    public async Task UpdateBusinessInfo()
    {
        var client = SharedTestResources.Factory.CreateClient();

        // login
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "test", Password = "test" }),
            Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
        LoginResponseDto? user = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);

        // check current business info
        var response = await client.GetAsync("/construction/api/info");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();

        GetBusinessInfoDto? businessInfo = JsonConvert.DeserializeObject<GetBusinessInfoDto>(responseString);

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

        // update business info and check if it was updated
        var updateBusinessInfoDto = new UpdateBusinessInfoDto
        {
            Name = "test2",
            Email = "test2",
            Phone = "test2",
            Address = "test2",
            City = "test2",
            Info = "test2",
            Facebook = "test2",
            Instagram = "test2",
            Youtube = "test2",
            Tiktok = "test2",
            Linkedin = "test2"
        };

        var updateResponse = await client.PatchAsync("/construction/api/info", new StringContent(JsonConvert.SerializeObject(updateBusinessInfoDto), Encoding.UTF8, "application/json"));
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var updatedResponseString = await updateResponse.Content.ReadAsStringAsync();

        GetBusinessInfoDto? updatedBusinessInfo = JsonConvert.DeserializeObject<GetBusinessInfoDto>(updatedResponseString);

        Assert.Equal("test2", updatedBusinessInfo!.Name);
        Assert.Equal("test2", updatedBusinessInfo!.Email);
        Assert.Equal("test2", updatedBusinessInfo!.Phone);
        Assert.Equal("test2", updatedBusinessInfo!.Address);
        Assert.Equal("test2", updatedBusinessInfo!.City);
        Assert.Equal("test2", updatedBusinessInfo!.Info);
        Assert.Equal("test2", updatedBusinessInfo!.Facebook);
        Assert.Equal("test2", updatedBusinessInfo!.Instagram);
        Assert.Equal("test2", updatedBusinessInfo!.Youtube);
        Assert.Equal("test2", updatedBusinessInfo!.Tiktok);
        Assert.Equal("test2", updatedBusinessInfo!.Linkedin);
    }

    [Fact]
    public async Task UpdateBusinessInfoWithInvalidToken()
    {
        var client = SharedTestResources.Factory.CreateClient();

        var updateBusinessInfoDto = new UpdateBusinessInfoDto
        {
            Name = "test2",
            Email = "test2",
            Phone = "test2",
            Address = "test2",
            City = "test2",
            Info = "test2",
            Facebook = "test2",
            Instagram = "test2",
            Youtube = "test2",
            Tiktok = "test2",
            Linkedin = "test2"
        };

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        var updateResponse = await client.PatchAsync("/construction/api/info", new StringContent(JsonConvert.SerializeObject(updateBusinessInfoDto), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.Unauthorized, updateResponse.StatusCode);
    }

    [Fact]
    public async Task UpdateBusinessInfoNoToken()
    {
        var client = SharedTestResources.Factory.CreateClient();

        var updateBusinessInfoDto = new UpdateBusinessInfoDto
        {
            Name = "test2",
            Email = "test2",
            Phone = "test2",
            Address = "test2",
            City = "test2",
            Info = "test2",
            Facebook = "test2",
            Instagram = "test2",
            Youtube = "test2",
            Tiktok = "test2",
            Linkedin = "test2"
        };

        var updateResponse = await client.PatchAsync("/construction/api/info", new StringContent(JsonConvert.SerializeObject(updateBusinessInfoDto), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.Unauthorized, updateResponse.StatusCode);
    }
}