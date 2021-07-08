using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using projectStructureApp.ModelsDTOapp;
using Xunit;

namespace projectStructure.WebAPI.IntegrationTests
{
    public class ProjectsControllerIntegrationTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        public ProjectsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task CreateUser_WhenCorrectData_ThenCreatedStatusCode()
        {
            var proj = new Common.DTOapp.ProjectCreateDTO()
            {
                AuthorId = 1,
                Deadline = new DateTime(2021, 9, 9),
                Description = "somedescript",
                Name = "name",
                TeamId = 1
            };
            string jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"https://localhost:44326/api/projects", data);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);


            response = await _client.GetAsync("https://localhost:44326/api/Projects");
            string strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<List<ProjectDTOapp>>(strResponse);
            Assert.IsType<List<ProjectDTOapp>>(responseObj);
            Assert.Equal(responseObj.Last().Name, proj.Name);
        }

        [Theory]
        [InlineData("{\"authorId\": 2,\"deadline\": \"2022-08-08\",\"description\": \"\",\"name\": \"\",\"teamId\": 1}")]
        [InlineData("{\"authorId\": 2,\"deadline\": \"2020-08-08\",\"description\": \"some text\",\"name\": \"name\",\"teamId\": 1}")]
        public async Task CreateProject_WhenWrongValidatedData_ThenThrowServerError(string jsonInString)
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync($"https://localhost:44326/api/projects", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        
    }
}
