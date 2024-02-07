using System.Net;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class GetAllJobsTests
{


    [Fact]
    public async Task GetAllJobs()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the jobs
        var response = await client.GetAsync("/construction/api/jobs");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        GetAllJobsDto[]? jobs = JsonConvert.DeserializeObject<GetAllJobsDto[]>(responseString);

        // check if the jobs are correct
        Assert.Equal(1, jobs![0].Job_Id);
        Assert.Equal("test", jobs![0].Title);
        Assert.Equal("test", jobs![0].Tagline);
        Assert.Equal("test", jobs![0].Description);
        Assert.Equal("test", jobs![0].Job_Type);
        Assert.Equal(new DateTime(2022, 1, 1), jobs![0].Date);
        Assert.Equal("test", jobs![0].Client);
        Assert.Equal("test", jobs![0].Location);

        Assert.Equal(2, jobs![1].Job_Id);
        Assert.Equal("test", jobs![1].Title);
        Assert.Equal("test", jobs![1].Tagline);
        Assert.Equal("test", jobs![1].Description);
        Assert.Equal("test", jobs![1].Job_Type);
        Assert.Equal(new DateTime(2022, 1, 1), jobs![1].Date);
        Assert.Equal("test", jobs![1].Client);
        Assert.Equal("test", jobs![1].Location);
    }
}
