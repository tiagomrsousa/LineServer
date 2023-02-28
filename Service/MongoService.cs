using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LineServer.Service
{
    public class MongoService : IFileService
    {
        //We can have another implementations of the IFileService interface. 
        //In this case the implemetation is based on a DB system.
        //This should be the solution when the file is too big to fit in the available memory, or at least until the point
        //all the server is compromissed due to lack of memory and constant page swap
        public string GetLine(int index)
        {
            throw new NotImplementedException();
        }
    }
}
