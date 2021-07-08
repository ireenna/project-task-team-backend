using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;
using projectStructureApp.ModelsDTOapp;
using Xunit;

namespace projectStructure.WebAPI.IntegrationTests
{
    public class UsersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        public UsersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task UpdateUser_WhenCorrectData_ThenOkStatusCode()
        {
            var proj = new UserUpdateDTO()
            {
                BirthDay = new DateTime(2020,01,01),
                Email = "Somemail@gmail.com",
                FirstName = "Somename",
                LastName = "lastname",
                TeamId = 1
            };
            string jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:44326/api/users/4", data);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync("https://localhost:44326/api/Users/4");
            string strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<User>(strResponse);
            Assert.Equal(responseObj.TeamId, proj.TeamId);
        }
        [Fact]
        public async Task UpdateUserTeamId_WhenWrongId_ThenInternalServerError()
        {
            var proj = new UserUpdateDTO()
            {
                BirthDay = new DateTime(2020, 01, 01),
                Email = "Somemail@gmail.com",
                FirstName = "Somename",
                LastName = "lastname",
                TeamId = -1
            };
            string jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:44326/api/users/4", data);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            response = await _client.GetAsync("https://localhost:44326/api/Users/4");
            string strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<User>(strResponse);
            Assert.NotEqual(responseObj.TeamId, proj.TeamId);
        }
        [Fact]
        public async Task GetUserTasks_ThenFinteredListOfTasks()
        {
            var response = await _client.GetAsync($"https://localhost:44326/api/users/1/tasks/filtered");
            var strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<List<Tasks>>(strResponse);
            Assert.NotNull(responseObj);
            Assert.Equal(4, responseObj.Count());
            Assert.Equal((1, 3, 5, 6), (responseObj[0].Id, responseObj[1].Id, responseObj[2].Id, responseObj[3].Id));
        }
        [Fact]
        public async Task GetUserTasks_WhenWrongId_ThenError()
        {
            var response = await _client.GetAsync($"https://localhost:44326/api/users/-1/tasks/filtered");
            Assert.Equal(HttpStatusCode.InternalServerError,response.StatusCode);
        }
        [Fact]
        public async Task GetUserUnfinishedTasks_ThenReceiveCorrectData()
        {
            var response = await _client.GetAsync($"https://localhost:44326/api/users/1/tasks/unfinished");
            var strResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<List<Tasks>>(strResponse);
            Assert.Equal(4, responseObj.Count());
            Assert.Equal((1, 3, 4, 6), (responseObj[0].Id, responseObj[1].Id, responseObj[2].Id, responseObj[3].Id));
        }
        [Fact]
        public async Task GetUserUnfinishedTasks_WhenWrongId_ThenBadRequest()
        {
            var response = await _client.GetAsync($"https://localhost:44326/api/users/-1/tasks/unfinished");
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
