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
            string filePath = "Resources/Text.txt";
            fileInfo = File.ReadAllLines(filePath);
        }

        public string GetLine(int index)
        {
            return fileInfo[index];
        }
    }
}
