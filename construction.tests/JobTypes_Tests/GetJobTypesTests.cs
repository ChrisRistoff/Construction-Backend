using System.Net;
using construction.Dtos;
using Newtonsoft.Json;

[Collection("Sequential")]
public class GetJobTypesTests
{
    [Fact]
    public async Task GetJobTypes()
    {
        var client = SharedTestResources.Factory.CreateClient();

        var response = await client.GetAsync("/construction/api/jobtypes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();

        List<GetJobTypeDto>? jobTypes = JsonConvert.DeserializeObject<List<GetJobTypeDto>>(responseString);

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
        var client = SharedTestResources.Factory.CreateClient();

        var response = await client.GetAsync("/construction/api/jobtypes/test");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();

        GetJobTypeDto? jobType = JsonConvert.DeserializeObject<GetJobTypeDto>(responseString);

        Assert.Equal("test", jobType!.Name);
        Assert.Equal("test", jobType!.Description);
        Assert.Equal("test", jobType!.Image);
        Assert.Equal("test", jobType!.Icon);
    }

    [Fact]
    public async Task GetJobTypeByName_NotFound()
    {
        var client = SharedTestResources.Factory.CreateClient();

        var response = await client.GetAsync("/construction/api/jobtypes/asfas");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}
