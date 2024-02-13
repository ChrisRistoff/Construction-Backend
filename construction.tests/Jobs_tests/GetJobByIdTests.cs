using System.Net;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class GetJobByIdTests
{



    [Fact]
    public async Task GetJobById()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job
        var response = await client.GetAsync("/construction/api/jobs/1");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // get the response string
        var responseString = await response.Content.ReadAsStringAsync();

        // deserialize the response string
        GetJobDto? job = JsonConvert.DeserializeObject<GetJobDto>(responseString);

        // check if the job is correct
        Assert.Equal(1, job!.Job_Id);
        Assert.Equal("test", job!.Title);
        Assert.Equal("test", job!.Tagline);
        Assert.Equal("test", job!.Description);
        Assert.Equal("test", job!.Job_Type);
        Assert.Equal(new DateTime(2022, 1, 1), job!.Date);
        Assert.Equal("test", job!.Client);
        Assert.Equal("test", job!.Location);

        Assert.Equal(1, job!.Images[0].Image_Id);
        Assert.Equal(1, job!.Images[0].Job_Id);
        Assert.Equal("test", job!.Images[0].Image);

        Assert.Equal(2, job!.Images[1].Image_Id);
        Assert.Equal(1, job!.Images[1].Job_Id);
        Assert.Equal("test2", job!.Images[1].Image);
    }



    [Fact]
    public async Task GetJobByIdNotFound()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job
        var response = await client.GetAsync("/construction/api/jobs/100");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }



    [Fact]
    public async Task GetJobByIdBadRequest()
    {

        // create a client
        var client = SharedTestResources.Factory.CreateClient();

        // get the job
        var response = await client.GetAsync("/construction/api/jobs/asd");

        // check if the status code is OK
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
