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

        // loop through the jobs and check they have all fields
        foreach (var job in jobs!)
        {
            Assert.NotNull(job.Title);
            Assert.NotNull(job.Tagline);
            Assert.NotNull(job.Description);
            Assert.NotNull(job.Job_Type);
            Assert.NotNull(job.Date);
            Assert.NotNull(job.Client);
            Assert.NotNull(job.Location);
        }
    }
}
