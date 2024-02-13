using System.Net;
using System.Net.Http.Json;
using construction.Dtos;
using Newtonsoft.Json;



[Collection("Sequential")]
public class EditJobByIdTests
{



        [Fact]
        public async Task EditJobById()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "updated",
                Tagline = "updated",
                Description = "updated",
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // get the response string
            var responseString = await response.Content.ReadAsStringAsync();

            // deserialize the response string
            EditJobDto? job = JsonConvert.DeserializeObject<EditJobDto>(responseString);

            // check if the job is correct
            Assert.Equal("updated", job!.Title);
            Assert.Equal("updated", job!.Tagline);
            Assert.Equal("updated", job!.Description);
            Assert.Equal("updated", job!.Client);
            Assert.Equal("updated", job!.Location);

            // revert back to original
            await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "test",
                Tagline = "test",
                Description = "test",
                Client = "test",
                Location = "test"
            });
        }



        [Fact]
        public async Task EditJobByIdNotFound()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/100", new EditJobDto
            {
                Title = "updated",
                Tagline = "updated",
                Description = "updated",
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }



        [Fact]
        public async Task EditJobByIdUnauthorized()
        {

            // create a client
            var client = SharedTestResources.Factory.CreateClient();

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "updated",
                Tagline = "updated",
                Description = "updated",
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }



        [Fact]
        public async Task EditJobByIdBadRequest_NoTitle()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = null!,
                Tagline = "updated",
                Description = "updated",
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }



        [Fact]
        public async Task EditJobByIdBadRequest_NoTagline()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "updated",
                Tagline = null!,
                Description = "updated",
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }



        [Fact]
        public async Task EditJobByIdBadRequest_NoDescription()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "updated",
                Tagline = "updated",
                Description = null!,
                Client = "updated",
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }



        [Fact]
        public async Task EditJobByIdBadRequest_NoClient()
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
            var loginResponse = await client.PostAsJsonAsync("/construction/api/login-admin", user);

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            // get the response string
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();

            // deserialize the response string
            var token = JsonConvert.DeserializeObject<LoginResponseDto>(loginResponseString);

            // add the token to the client
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token!.Token);

            // edit the job
            var response = await client.PatchAsJsonAsync("/construction/api/jobs/1", new EditJobDto
            {
                Title = "updated",
                Tagline = "updated",
                Description = "updated",
                Client = null!,
                Location = "updated"
            });

            // check if the status code is OK
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
}
