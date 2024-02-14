using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class CreateJobTests
{


    [Fact]
    public async Task CreateJob_ValidJob_ReturnsJob()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // admin
        var admin = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login
        var response = await client.PostAsJsonAsync("construction/api/login-admin", admin);

        // check status code
        response.EnsureSuccessStatusCode();

        // get token from response
        var loginContent = await response.Content.ReadAsStringAsync();

        // deserialize token
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginContent)!.Token;

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "test",
            Date = DateTime.Now,
            Client = "Test Client",
            Location = "Test Location"
        };

        // create job
        response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        response.EnsureSuccessStatusCode();

        // check content
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AddJobDto>(content);

        // check job
        Assert.Equal(job.Title, result!.Title);
        Assert.Equal(job.Tagline, result.Tagline);
        Assert.Equal(job.Description, result.Description);
        Assert.NotNull(result.Date);
        Assert.Equal(job.Job_Type, result.Job_Type);
        Assert.Equal(job.Client, result.Client);
        Assert.Equal(job.Location, result.Location);
    }



    [Fact]
    public async Task CreateJob_MissingDate_ReturnsWithCurrentDate()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // admin
        var admin = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login
        var response = await client.PostAsJsonAsync("construction/api/login-admin", admin);

        // check status code
        response.EnsureSuccessStatusCode();

        // get token from response
        var loginContent = await response.Content.ReadAsStringAsync();

        // deserialize token
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginContent)!.Token;

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "test",
            Client = "Test Client",
            Location = "Test Location"
        };

        // create job
        response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        response.EnsureSuccessStatusCode();

        // check content
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AddJobDto>(content);

        // check job
        Assert.Equal(job.Title, result!.Title);
        Assert.Equal(job.Tagline, result.Tagline);
        Assert.Equal(job.Description, result.Description);
        Assert.NotNull(result.Date);
        Assert.Equal(job.Job_Type, result.Job_Type);
        Assert.Equal(job.Client, result.Client);
        Assert.Equal(job.Location, result.Location);
    }



    [Fact]
    public async Task CreateJob_InvalidJob_ReturnsBadRequest()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // admin
        var admin = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login
        var response = await client.PostAsJsonAsync("construction/api/login-admin", admin);

        // check status code
        response.EnsureSuccessStatusCode();

        // get token from response
        var loginContent = await response.Content.ReadAsStringAsync();

        // deserialize token
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginContent)!.Token;

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "non existent job type",
            Date = DateTime.Now,
            Client = "Test Client",
            Location = "Test Location"
        };

        // create job
        response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }



    [Fact]
    public async Task CreateJob_Unauthorized_ReturnsUnauthorized()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "test",
            Date = DateTime.Now,
            Client = "Test Client",
            Location = "Test Location"
        };

        // create job
        var response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }



    [Fact]
    public async Task CreateJob_InvalidToken_ReturnsUnauthorized()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid token");

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "test",
            Date = DateTime.Now,
            Client = "Test Client",
            Location = "Test Location"
        };

        // create job
        var response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }



    [Fact]
    public async Task CreateJob_MissingFields_ReturnsBadRequest()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // admin
        var admin = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login
        var response = await client.PostAsJsonAsync("construction/api/login-admin", admin);

        // check status code
        response.EnsureSuccessStatusCode();

        // get token from response
        var loginContent = await response.Content.ReadAsStringAsync();

        // deserialize token
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginContent)!.Token;

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // create job
        var job = new AddJobDto()
        {
            Title = "Test Job",
            Tagline = "Test Tagline",
            Description = "Test Description",
            Job_Type = "test",
            Date = DateTime.Now,
            Client = "Test Client"
        };

        // create job
        response = await client.PostAsJsonAsync("construction/api/jobs", job);

        // check status code
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
