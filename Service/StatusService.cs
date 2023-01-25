using LineServer.Models;

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
