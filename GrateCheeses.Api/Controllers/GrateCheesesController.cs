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

        // GET api/v1/<gratecheeses>
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
                _logger.LogError(ex, "Something went wrong processing this request for all cheese.");

                return BadRequest();
            }
        }



        // GET api/v1/<gratecheeses>/5
        [HttpGet("{id}")]
        public ActionResult<Cheese> Get(int id)
        {
            try
            {
                var cheese = _cheeseData.GetCheeseByCheeseId(id);

                if (cheese == null)
                {
                    _logger.LogError($"Unabled to find a cheese with id {id}");

                    return NotFound($"Unabled to find a cheese with id {id}");
                }

                return Ok(cheese);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong processing this request for a specific cheese.");

                return BadRequest();
            }
        }

        // POST api/v1/<gratecheeses>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<test>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<test>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
