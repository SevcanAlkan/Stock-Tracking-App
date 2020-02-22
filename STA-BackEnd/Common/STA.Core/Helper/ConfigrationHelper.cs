using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STA.Core.Helper
{
    public static class ConfigrationHelper
    {
        public static IConfiguration Get(string path)
        {
            return new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), path))
               .AddJsonFile("appsettings.json")
               .Build();
        }
    }
}
