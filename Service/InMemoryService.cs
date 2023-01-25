using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

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

            try
            {
                fileInfo = File.ReadAllLines(pathValue);
            }
            catch (DirectoryNotFoundException)
            {
                fileInfo = new string[0];
            }
        }

        public string GetLine(int index)
        {
            return fileInfo[index];
        }
    }
}
