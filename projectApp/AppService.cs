﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp
{
    public sealed class AppService
    {
        private readonly HttpClient appClient = new HttpClient() { BaseAddress = new Uri("https://localhost:44326/api/") };
        public async Task<Boolean> GetAll()
        {
            
        }
    }
}
