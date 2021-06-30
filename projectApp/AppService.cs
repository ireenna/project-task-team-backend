using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructureApp.ModelsDTOapp;
using projectStructureApp.ModelsDTOapp.Create;

namespace projectStructureApp
{
    public sealed class AppService
    {
        private readonly HttpClient appClient = new HttpClient() { BaseAddress = new Uri("https://localhost:44326/api/") };

        public async Task<Action> RefreshData()
        {
            HttpResponseMessage response = await appClient.GetAsync("Data");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Action>(strResponse);
        }
        public async Task<List<ProjectDTOapp>> GetAllProjects()
        {
            HttpResponseMessage response = await appClient.GetAsync("Projects");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectDTOapp>>(strResponse);
        }
        public async Task<Dictionary<ProjectDTOapp, int>> GetQuantityOfUserTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync("info/tasks/"+id);
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<ProjectDTOapp, int>>(strResponse);
        }
        public async Task<List<TasksDTOapp>> GetUserTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"info/users/{id}/tasks");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TasksDTOapp>>(strResponse);
        }
        public async Task<List<(int id, string name)>> GetUserFinishedTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"info/users/{id}/tasks/finished");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<(int id, string name)>>(strResponse);
        }
        public async Task<List<(int id, string name, List<UserDTOapp> users)>> GetSortedUsersTeams()
        {
            HttpResponseMessage response = await appClient.GetAsync($"info/teams/sorted");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<(int id, string name, List<UserDTOapp> users)>>(strResponse);
        }
        public async Task<List<List<TasksDTOapp>>> GetSortedUsersWithTasks()
        {
            HttpResponseMessage response = await appClient.GetAsync($"info/users/sorted");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject< List<List<TasksDTOapp>>>(strResponse);
        }
        public async Task<UserTaskInfoDTOapp> GetUserTaskInfo(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"Info/users/{id}/tasks/info");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserTaskInfoDTOapp>(strResponse);
        }
        public async Task<List<ProjectsInfoDTOapp>> GetProjectsInfo()
        {
            HttpResponseMessage response = await appClient.GetAsync($"Info/projects/info");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectsInfoDTOapp>>(strResponse);
        }
        public async Task<Action> CreateProject(ProjectCreateApp proj)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PostAsync($"projects", data);
            if (response.StatusCode == HttpStatusCode.Created)
                return JsonConvert.DeserializeObject<Action>(await response.Content.ReadAsStringAsync());
            throw new Exception();
        }
    }
}
