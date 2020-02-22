using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NGA.Data.Helper
{
    public static class ConfigrationHelper
    {
        public static IConfiguration Get()
        {
            return new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NGA.MonolithAPI/"))
               .AddJsonFile("appsettings.json")
               .Build();
        }
    }
}
