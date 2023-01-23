using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LineServer.Service
{
    public class InMemoryService : IFileService
    {
        private static string[] fileInfo;

        public static string[] FileInfo { get => fileInfo; set => fileInfo = value; }

        static InMemoryService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            string pathValue;
            if(!configuration.Providers.First().TryGet("AppSettings:FilePath", out pathValue))
            {
                pathValue = "Resources/Text.txt"; //default value in case nothing configured
            }

            fileInfo = File.ReadAllLines(pathValue);
        }

        public string GetLine(int index)
        {
            return fileInfo[index];
        }
    }
}
