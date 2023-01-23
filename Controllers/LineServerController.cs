using LineServer.Models;
using LineServer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LineServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineServerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LineServerController> _logger;

        private static readonly IFileService fileService = new InMemoryService();
        private static readonly StatusService statusService = new StatusService();

        public LineServerController(ILogger<LineServerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get Line information
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">OK</response>
        /// <response code="413">Request line is beyond the end of the file</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("lines/{line}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.RequestEntityTooLarge, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IActionResult GetLine([FromRoute] int line)
        {
            try
            {
                return Ok(fileService.GetLine(line));
            }
            catch(IndexOutOfRangeException e)
            {
                _logger.LogError(e.Message);
                //maybe the best error code to return here should be a 404 - Not Found, because we are trying to retrieve something that doesn't exists.
                //but is just my opinion
                return StatusCode(413, e.Message); 
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get the server status
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("status")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(StatusInfo))]
        public IActionResult GetStatus()
        {
           return Ok(statusService.GetStatus());
        }
    }
}
