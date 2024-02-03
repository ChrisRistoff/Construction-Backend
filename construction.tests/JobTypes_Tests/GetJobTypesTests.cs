using System.Net;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class GetJobTypesTests
{



    [Fact]
    public async Task GetJobTypes()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job types
        var response = await client.GetAsync("/construction/api/jobtypes");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        List<GetJobTypeDto>? jobTypes = JsonConvert.DeserializeObject<List<GetJobTypeDto>>(responseString);

        // check if the job types are correct
        Assert.Equal("test", jobTypes![0].Name);
        Assert.Equal("test", jobTypes![0].Description);
        Assert.Equal("test", jobTypes![0].Image);
        Assert.Equal("test", jobTypes![0].Icon);

        Assert.Equal("test2", jobTypes![1].Name);
        Assert.Equal("test2", jobTypes![1].Description);
        Assert.Equal("test2", jobTypes![1].Image);
        Assert.Equal("test2", jobTypes![1].Icon);

        Assert.Equal("test3", jobTypes![2].Name);
        Assert.Equal("test3", jobTypes![2].Description);
        Assert.Equal("test3", jobTypes![2].Image);
        Assert.Equal("test3", jobTypes![2].Icon);
    }



    [Fact]
    public async Task GetJobTypeByName()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job type by name
        var response = await client.GetAsync("/construction/api/jobtypes/test");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        GetJobTypeDto? jobType = JsonConvert.DeserializeObject<GetJobTypeDto>(responseString);

        // check if the job type is correct
        Assert.Equal("test", jobType!.Name);
        Assert.Equal("test", jobType!.Description);
        Assert.Equal("test", jobType!.Image);
        Assert.Equal("test", jobType!.Icon);
    }



    [Fact]
    public async Task GetJobTypeByName_NotFound()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job type by name
        var response = await client.GetAsync("/construction/api/jobtypes/asfas");

        // check if the status code is NotFound
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}
