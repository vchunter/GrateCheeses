using GrateCheeses.Api.Models;
using GrateCheeses.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace GrateCheeses.Api.Controllers
{
    //TODO: future state, make the api version control happen in the request headers instead of the api url
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
        public ActionResult<Cheese> Post(Cheese cheese)
        {
            try
            {
                if (cheese == null)
                {
                    _logger.LogError($"There is a problem with the cheese you are trying to create, it doesn't appear to exist", cheese);

                    return BadRequest($"There is a problem with the cheese you are trying to create, it doesn't appear to exist");
                }

                //TODO: future state, improve this so that the user doesn't have to submit a CheeseId
                //TODO: separate out the image for the cheese into a separate call so we can upload the file as well
                var newCheese = _cheeseData.AddCheese(cheese);

                return Ok(newCheese);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to add this cheese");

                return BadRequest();
            }
        }

        // POST api/v1/<gratecheeses>/{id}/cost/{weight}
        [HttpPost("{id}/Cost/{weightGrams}")]
        public ActionResult<float> Cost(int id, float weightGrams)
        {
            try
            {
                if (id == 0 || weightGrams == 0)
                {
                    _logger.LogError($"Sorry, we can't calculate a cost without a cheese select or a weight");

                    return BadRequest($"Sorry, we can't calculate a cost without a cheese select or a weight");
                }
                                
                //TODO: future state, might be able to pull out the calculation cost into its own class in the future
                var cheeseCost = _cheeseData.GetCheeseByCheeseId(id).CostByWeight(weightGrams);

                return Ok(cheeseCost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to calculate the cost of the cheese");

                return BadRequest();
            }
        }

        // PUT api/v1/<gratecheeses>/5
        [HttpPut("{id}")]
        public ActionResult<Cheese> Put(int id, Cheese cheese)
        {
            try
            {
                if (cheese == null)
                {
                    _logger.LogError($"There is a problem with the cheese you are trying to update, it doesn't appear to exist", cheese);

                    return BadRequest($"There is a problem with the cheese you are trying to update, it doesn't appear to exist");
                }

                //TODO: implement some validation in the future to prevent a user from updating the CheeseId
                var updatedCheese = _cheeseData.UpdateCheese(cheese);

                return Ok(updatedCheese);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to update this cheese");

                return BadRequest();
            }
        }

        // DELETE api/v1/<gratecheeses>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var cheese = _cheeseData.GetCheeseByCheeseId(id);

                if (cheese == null)
                {
                    _logger.LogError($"Unabled to find a cheese with id {id}");

                    return NotFound($"Unabled to find a cheese with id {id}");
                }

                var cheeseRemoved = _cheeseData.DeleteCheese(id);

                if (!cheeseRemoved)
                {
                    _logger.LogError($"There was a problem trying to remove the cheese with id {id}");

                    return BadRequest($"There was a problem trying to remove the cheese with id {id}");
                }

                return Ok(cheeseRemoved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when trying to remove this cheese");

                return BadRequest();
            }
        }
    }
}
