using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class DeleteJobTests
{


    [Fact]
    public async Task DeleteJob_ValidJob_ReturnsJob()
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

        // get job from response
        var jobContent = await response.Content.ReadAsStringAsync();

        // deserialize job
        GetJobDto? createdJob = JsonConvert.DeserializeObject<GetJobDto>(jobContent);

        // delete job
        response = await client.DeleteAsync($"construction/api/jobs/{createdJob!.Job_Id}");

        // ensure no content status code
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // get the deleted job
        response = await client.GetAsync($"construction/api/jobs/{createdJob!.Job_Id}");

        // ensure not found status code
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
