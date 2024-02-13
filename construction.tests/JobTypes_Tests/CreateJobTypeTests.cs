using System.Net;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class CreateJobTypesTests
{


    [Fact]
    public async Task CreateJobType()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as admin
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // add the token to the client
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Name = "test333",
            Description = "test333",
            Image = "test",
            Icon = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        AddJobTypeDto? createdJobType = JsonConvert.DeserializeObject<AddJobTypeDto>(responseString);

        // check if the job type is correct
        Assert.Equal("test333", createdJobType!.Name);
        Assert.Equal("test333", createdJobType!.Description);
        Assert.Equal("test", createdJobType!.Image);
        Assert.Equal("test", createdJobType!.Icon);
    }



    [Fact]
    public async Task CreateJobTypeWithoutToken()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Name = "test333",
            Description = "test333",
            Image = "test",
            Icon = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }


    [Fact]
    public async Task CreateJobTypeWithoutName()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as admin
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // add the token to the client
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Description = "test333",
            Image = "test",
            Icon = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task CreateJobTypeWithoutDescription()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as admin
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // add the token to the client
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Name = "test333",
            Image = "test",
            Icon = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }



    [Fact]
    public async Task CreateJobTypeWithoutImage()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as admin
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // add the token to the client
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Name = "test333",
            Description = "test333",
            Icon = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }



    [Fact]
    public async Task CreateJobTypeWithoutIcon()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as admin
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // get the response string
        var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

        // add the token to the client
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

        // create a job type
        var jobType = new AddJobTypeDto
        {
            Name = "test333",
            Description = "test333",
            Image = "test"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
