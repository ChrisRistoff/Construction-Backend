using System.Net;
using System.Net.Http.Headers;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class TestAuth
{

    // intialize the client for internal use
    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();



    [Fact]
    public async Task TestProtectedEndpoint()
    {

        // login user
        var loginResponse = await _client.PostAsync("/construction/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "test", Password = "test" }),
            Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        LoginResponseDto? user = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // set the token in the client headers
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.Token);

        // get the protected endpoint
        var response = await _client.GetAsync("construction/api/test-auth");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }



    [Fact]
    public async Task TestProtectedEndpoint_WithInvalidToken()
    {

        // set the token in the client headers to an invalid token
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        // get the protected endpoint
        var response = await _client.GetAsync("construction/api/test-auth");

        // check if the status code is Unauthorized
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }



    [Fact]
    public async Task LoginWithWrongPassword()
    {

        // login user with wrong password
        var loginResponse = await _client.PostAsync("construction/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "test", Password = "wrong-password" }),
            Encoding.UTF8, "application/json"));

        // check if the status code is BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);

        // check the response string
        Assert.Equal("Username or password is incorrect", await loginResponse.Content.ReadAsStringAsync());
    }



    [Fact]
    public async Task LoginWithWrongUsername()
    {

        // login user with wrong username
        var loginResponse = await _client.PostAsync("construction/api/login-admin", new StringContent(
            JsonConvert.SerializeObject(new LoginRequestDto { Name = "wrong-username", Password = "test" }),
            Encoding.UTF8, "application/json"));

        // check if the status code is BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);

        // check the response string
        Assert.Equal("Username or password is incorrect", await loginResponse.Content.ReadAsStringAsync());
    }
}
