using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Service
{
    public class AppSettings
    {
        public IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string conString()
        {
            return _configuration["ConnectionStrings:DefaultConnection"];
        }
    }
}
