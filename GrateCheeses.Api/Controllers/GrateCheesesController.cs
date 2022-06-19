using GrateCheeses.Api.Models;
using GrateCheeses.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace GrateCheeses.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GrateCheesesController : ControllerBase
    {
        private readonly ILogger<GrateCheesesController> _logger;
        private readonly ICheeseData _cheeseData;

        public GrateCheesesController(ILogger<GrateCheesesController> logger, ICheeseData cheeseData)
        {
            _logger = logger;
            _cheeseData = cheeseData;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Cheese>> Get()
        {
            try
            {
                var cheeseList = _cheeseData.GetAllCheeses();

                return Ok(cheeseList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong processing this request.");

                return BadRequest();
            }
        }
    }
}
