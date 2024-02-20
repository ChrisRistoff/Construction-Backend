using System.Net;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class DeleteJobTypeTests
{

    [Fact]
    public async Task DeleteJobType()
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
            Name = "toDelete",
            Description = "toDelete",
            Image = "toDelete",
            Icon = "toDelete"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // delete the job type
        var deleteResponse = await client.DeleteAsync("/construction/api/jobtypes/toDelete");

        // check if the status code is NoContent
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // get the job type
        var getResponse = await client.GetAsync("/construction/api/jobtypes/toDelete");

        // check if the status code is NotFound
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }



    [Fact]
    public async Task DeleteJobTypeWithInvalidName()
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

        // delete the job type
        var deleteResponse = await client.DeleteAsync("/construction/api/jobtypes/invalidName");

        // check if the status code is NotFound
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }



    [Fact]
    public async Task DeleteJobTypeWithoutToken()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // delete the job type
        var deleteResponse = await client.DeleteAsync("/construction/api/jobtypes/invalidName");

        // check if the status code is Unauthorized
        Assert.Equal(HttpStatusCode.Unauthorized, deleteResponse.StatusCode);
    }



    [Fact]
    public async Task DeleteJobTypeWithoutAdminToken()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // user creadintials
        var user = new LoginRequestDto()
        {
            Name = "test",
            Password = "test"
        };

        // login as user
        var loginResponse = await client.PostAsync("/construction/api/login-admin", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // delete the job type
        var deleteResponse = await client.DeleteAsync("/construction/api/jobtypes/test");

        // check if the status code is Unauthorized
        Assert.Equal(HttpStatusCode.Unauthorized, deleteResponse.StatusCode);
    }
}
