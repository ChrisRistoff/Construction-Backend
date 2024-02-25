using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using construction.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



[Collection("Sequential")]
public class DeleteImageForJobTests
{


    [Fact]
    public async Task DeleteImageForJob_ValidImage_ReturnsImage()
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

        // delete image request
        var deleteImage = await client.DeleteAsync("construction/api/jobs/image/4");

        // check status code
        Assert.Equal(HttpStatusCode.NoContent, deleteImage.StatusCode);

        // get job
        response = await client.GetAsync("construction/api/jobs/1");

        // check status code
        response.EnsureSuccessStatusCode();

        // get job from response
        var jobContent = await response.Content.ReadAsStringAsync();

        // deserialize job
        GetJobDto job = JsonConvert.DeserializeObject<GetJobDto>(jobContent)!;

        // check all images and ensure image is deleted
        foreach (var jobImage in job.Images!)
        {
            Assert.NotEqual(4, jobImage.Image_Id);
        }
    }



    [Fact]
    public async Task DeleteImageForJob_InvalidImage_ReturnsNotFound()
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

        // create a new HTTP request
        var request = new HttpRequestMessage(HttpMethod.Delete, "construction/api/jobs/image/99999999");

        // send request
        var deletedImageResponse = await client.SendAsync(request);

        // check status code
        Assert.Equal(HttpStatusCode.NotFound, deletedImageResponse.StatusCode);
    }



    [Fact]
    public async Task DeleteImageForJob_Unauthorized_ReturnsUnauthorized()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // create a new HTTP request
        var request = new HttpRequestMessage(HttpMethod.Delete, "construction/api/jobs/image/2");

        // send request
        var deletedImageResponse = await client.SendAsync(request);

        // check status code
        Assert.Equal(HttpStatusCode.Unauthorized, deletedImageResponse.StatusCode);
    }



    [Fact]
    public async Task DeleteImageForJob_InvalidToken_ReturnsUnauthorized()
    {

        // client
        var client = SharedTestResources.Factory.CreateClient();

        // set token
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid token");

        // create a new HTTP request
        var request = new HttpRequestMessage(HttpMethod.Delete, "construction/api/jobs/image/2");

        // send request
        var deletedImageResponse = await client.SendAsync(request);

        // check status code
        Assert.Equal(HttpStatusCode.Unauthorized, deletedImageResponse.StatusCode);
    }
}
