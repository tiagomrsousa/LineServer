using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineServer.Service
{
    interface IFileService
    {
        string GetLine(int index);
    }
}
