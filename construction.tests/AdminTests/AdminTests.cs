using System.Net;
using System.Net.Http.Headers;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;

[Collection("Sequential")]
public class TestAuth
{
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();

    [Fact]
    public async Task TestProtectedEndpoint()
    {
        var loginResponse = await _client.PostAsync("/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "test", Password = "test" }),
            Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        LoginResponseDto? user = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        Console.WriteLine("---------------------------------------------------------------------------------------");
        Console.WriteLine(user!.Token + " Token");
        Console.WriteLine("---------------------------------------------------------------------------------------");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);
        var response = await _client.GetAsync("/api/test-auth");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task TestProtectedEndpointWithInvalidToken()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        var response = await _client.GetAsync("/api/test-auth");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task LoginWithWrongPassword()
    {
        var loginResponse = await _client.PostAsync("/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "test", Password = "wrong-password" }),
            Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
        Assert.Equal("Username or password is incorrect", await loginResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task LoginWithWrongUsername()
    {
        var loginResponse = await _client.PostAsync("/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "wrong-username", Password = "test" }),
            Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
        Assert.Equal("Username or password is incorrect", await loginResponse.Content.ReadAsStringAsync());
    }
}
