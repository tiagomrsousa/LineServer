using LineServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineServer.Service
{
    public class StatusService
    {
        public StatusInfo GetStatus()
        {
            StatusInfo status = new StatusInfo();
            //Do any checks to external dependencies and compose the Statusnfo object with that information
            status.State = Status.OK.ToString();

            return status;
        }
    }
}
