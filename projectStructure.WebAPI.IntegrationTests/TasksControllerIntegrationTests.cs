using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructure.DAL;
using Xunit;

namespace projectStructure.WebAPI.IntegrationTests
{
    public class TasksControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        public TasksControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task DeleteTask_WhenCorrectId_ThenOkStatusCodeAndDoesntContain()
        {
            var response = await _client.GetAsync($"https://localhost:44326/api/tasks/1");
            string strResponse = await response.Content.ReadAsStringAsync();
            var task1 = JsonConvert.DeserializeObject<Tasks>(strResponse);

            response = await _client.DeleteAsync($"https://localhost:44326/api/tasks/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync("https://localhost:44326/api/tasks");
            strResponse = await response.Content.ReadAsStringAsync();
            var taskList = JsonConvert.DeserializeObject<List<Tasks>>(strResponse);
            Assert.DoesNotContain(task1, taskList);
        }
        [Theory]
        [InlineData(8)]
        [InlineData(-1)]
        public async Task DeleteTask_WhenWrongDataOrTaskDoesntExist_ThenBadRequest(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:44326/api/tasks/{id}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
