using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using projectStructure.Common;
using projectStructure.Common.Models;

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
        public async Task<List<Project>> GetAllProjects()
        {
            HttpResponseMessage response = await appClient.GetAsync("Projects");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Project>>(strResponse);
        }
        public async Task<Dictionary<Project, int>> GetQuantityOfUserTasks(int id)
        {
            HttpResponseMessage response = await appClient.GetAsync("Info/tasks/"+id);
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<Project, int>>(strResponse);
        }
    }
}
