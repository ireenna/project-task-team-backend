using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructureApp.ModelsApp.Create;
using projectStructureApp.ModelsApp.Update;
using projectStructureApp.ModelsDTOapp;
using projectStructureApp.ModelsDTOapp.Create;
using projectStructureApp.ModelsDTOapp.Update;

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
        public async Task<Dictionary<FullProjectDTOapp, int>> GetQuantityOfUserTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"users/{id}/project/alltasks");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<FullProjectDTOapp, int>>(strResponse);
        }
        public async Task<List<FullTasksDTOapp>> GetUserTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"users/{id}/tasks/filtered");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<FullTasksDTOapp>>(strResponse);
        }
        public async Task<List<(int id, string name)>> GetUserFinishedTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"users/{id}/tasks/finished");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<(int id, string name)>>(strResponse);
        }
        public async Task<List<(int id, string name, List<UserDTOapp> users)>> GetSortedUsersTeams()
        {
            HttpResponseMessage response = await appClient.GetAsync($"teams/sorted");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<(int id, string name, List<UserDTOapp> users)>>(strResponse);
        }
        public async Task<List<List<FullTasksDTOapp>>> GetSortedUsersWithTasks()
        {
            HttpResponseMessage response = await appClient.GetAsync($"users/tasks/sorted");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject< List<List<FullTasksDTOapp>>>(strResponse);
        }
        public async Task<UserTaskInfoDTOapp> GetUserTaskInfo(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync($"users/{id}/tasks/info");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserTaskInfoDTOapp>(strResponse);
        }
        public async Task<List<ProjectsInfoDTOapp>> GetProjectsInfo()
        {
            HttpResponseMessage response = await appClient.GetAsync($"projects/info");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectsInfoDTOapp>>(strResponse);
        }
        #region projects
        public async Task<List<ProjectDTOapp>> GetAllProjects()
        {
            HttpResponseMessage response = await appClient.GetAsync("Projects");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectDTOapp>>(strResponse);
        }
        public async Task<ProjectDTOapp> GetProject(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync("Projects/"+id);
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectDTOapp>(strResponse);
        }
        public async Task<Boolean> CreateProject(ProjectCreateApp proj)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PostAsync($"projects", data);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> UpdateProject(ProjectUpdateApp proj, int id)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PutAsync($"projects/{id}", data);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> DeleteProject(int id)
        {
            HttpResponseMessage response = await appClient.DeleteAsync("Projects/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        #endregion projects
        #region tasks
        public async Task<List<TasksDTOapp>> GetAllTasks()
        {
            HttpResponseMessage response = await appClient.GetAsync("Tasks");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TasksDTOapp>>(strResponse);
        }
        public async Task<TasksDTOapp> GetTask(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync("Tasks/" + id);
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TasksDTOapp>(strResponse);
        }
        public async Task<Boolean> CreateTask(TasksCreateApp proj)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PostAsync($"tasks", data);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> UpdateTask(TasksUpdateApp proj, int id)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PutAsync($"tasks/{id}", data);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> DeleteTask(int id)
        {
            HttpResponseMessage response = await appClient.DeleteAsync("tasks/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        #endregion tasks
        #region teams
        public async Task<List<TeamDTOapp>> GetAllTeams()
        {
            HttpResponseMessage response = await appClient.GetAsync("Teams");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TeamDTOapp>>(strResponse);
        }
        public async Task<TeamDTOapp> GetTeam(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync("Teams/" + id);
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TeamDTOapp>(strResponse);
        }
        public async Task<Boolean> CreateTeam(TeamCreateApp proj)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PostAsync($"teams", data);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> UpdateTeam(TeamUpdateApp proj, int id)
        {
            var jsonProj = JsonConvert.SerializeObject(proj);
            var data = new StringContent(jsonProj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await appClient.PutAsync($"teams/{id}", data);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        public async Task<Boolean> DeleteTeam(int id)
        {
            HttpResponseMessage response = await appClient.DeleteAsync("teams/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            throw new Exception();
        }
        #endregion teams
    }
}
