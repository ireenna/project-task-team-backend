using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructure.Common.DTOapp.Create;
using projectStructure.DAL;
using Xunit;

namespace projectStructure.WebAPI.IntegrationTests
{
    public class TeamsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        public TeamsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task CreateTeam_WhenCorrectData_ThenCreatedStatusCode()
        {
            var proj = new TeamCreateDTO()
            {
                Name = "new team"
            };
            string jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"https://localhost:44326/api/teams", data);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await _client.GetAsync("https://localhost:44326/api/teams");
            string strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<List<Team>>(strResponse);
            Assert.Equal(responseObj.Last().Name, proj.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData("{\"name\": \"\"}")]
        public async Task CreateTeam_WhenWrongData_ThenBadRequest(string jsonInString)
        {
            var response = await _client.PostAsync($"https://localhost:44326/api/teams", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
