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
}
