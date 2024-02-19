using System.Net;
using System.Text;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class EditJobTypeTests
{

    [Fact]
    public async Task EditJobType()
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
            Name = "test5",
            Description = "test5",
            Image = "test5",
            Icon = "test5"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // create a job type to edit
        var editJobType = new EditJobTypeDto
        {
            Description = "testEdit",
            Icon = "testEdit"
        };

        // serialize the job type
        var editJobTypeJson = JsonConvert.SerializeObject(editJobType);

        // create the content
        var editContent = new StringContent(editJobTypeJson, Encoding.UTF8, "application/json");

        // edit the job type
        var editResponse = await client.PatchAsync("/construction/api/jobtypes/test5", editContent);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);

        // check if the job type is correct
        var editResponseString = await editResponse.Content.ReadAsStringAsync();

        // deserialize the response string
        GetJobTypeDto? editedJobType = JsonConvert.DeserializeObject<GetJobTypeDto>(editResponseString);

        // check if the job type is correct
        Assert.Equal("test5", editedJobType!.Name);
        Assert.Equal("testEdit", editedJobType!.Description);
        Assert.Equal("test5", editedJobType!.Image);
        Assert.Equal("testEdit", editedJobType!.Icon);
    }



    [Fact]
    public async Task EditJobTypeWithoutToken()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // create a job type to edit
        var editJobType = new EditJobTypeDto
        {
            Description = "testEdit",
            Icon = "testEdit"
        };

        // serialize the job type
        var editJobTypeJson = JsonConvert.SerializeObject(editJobType);

        // create the content
        var editContent = new StringContent(editJobTypeJson, Encoding.UTF8, "application/json");

        // edit the job type
        var editResponse = await client.PatchAsync("/construction/api/jobtypes/test", editContent);

        // check if the status code is Unauthorized
        Assert.Equal(HttpStatusCode.Unauthorized, editResponse.StatusCode);
    }



    [Fact]
    public async Task EditJobTypeWithInvalidName()
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

        // create a job type to edit
        var editJobType = new EditJobTypeDto
        {
            Description = "testEdit",
            Icon = "testEdit"
        };

        // serialize the job type
        var editJobTypeJson = JsonConvert.SerializeObject(editJobType);

        // create the content
        var editContent = new StringContent(editJobTypeJson, Encoding.UTF8, "application/json");

        // edit the job type
        var editResponse = await client.PatchAsync("/construction/api/jobtypes/testInvalidName", editContent);

        // check if the status code is NotFound
        Assert.Equal(HttpStatusCode.NotFound, editResponse.StatusCode);
    }



    [Fact]
    public async Task EditJobTypeWithoutDescription()
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
            Name = "test6",
            Description = "test6",
            Image = "test6",
            Icon = "test6"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // create a job type to edit
        var editJobType = new EditJobTypeDto
        {
            Icon = "testEdit"
        };

        // serialize the job type
        var editJobTypeJson = JsonConvert.SerializeObject(editJobType);

        // create the content
        var editContent = new StringContent(editJobTypeJson, Encoding.UTF8, "application/json");

        // edit the job type
        var editResponse = await client.PatchAsync("/construction/api/jobtypes/test6", editContent);

        // check if the status code is BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, editResponse.StatusCode);
    }



    [Fact]
    public async Task EditJobTypeWithoutIcon()
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
            Name = "test7",
            Description = "test7",
            Image = "test7",
            Icon = "test7"
        };

        // serialize the job type
        var jobTypeJson = JsonConvert.SerializeObject(jobType);

        // create the content
        var content = new StringContent(jobTypeJson, Encoding.UTF8, "application/json");

        // create the job type
        var response = await client.PostAsync("/construction/api/jobtypes", content);

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // create a job type to edit
        var editJobType = new EditJobTypeDto
        {
            Description = "testEdit"
        };

        // serialize the job type
        var editJobTypeJson = JsonConvert.SerializeObject(editJobType);

        // create the content
        var editContent = new StringContent(editJobTypeJson, Encoding.UTF8, "application/json");

        // edit the job type
        var editResponse = await client.PatchAsync("/construction/api/jobtypes/test7", editContent);

        // check if the status code is BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, editResponse.StatusCode);
    }
}
