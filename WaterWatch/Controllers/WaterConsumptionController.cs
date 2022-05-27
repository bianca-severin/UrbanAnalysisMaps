using WaterWatch.Repositories;
using WaterWatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace WaterWatch.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class WaterConsumptionController: ControllerBase
    {
        private readonly IWaterConsumptionRepository _waterConsumptionRepository;

        //Constructor
        public WaterConsumptionController(IWaterConsumptionRepository waterConsumptionRepository)
        {
            _waterConsumptionRepository = waterConsumptionRepository;
        }

        [HttpGet("/waterconsumption/getall")]
        //function will accept an http GET request and return all of our data in json format
        public async Task<ActionResult<IEnumerable<WaterConsumption>>> GetAll()
        {
            var wcData = await _waterConsumptionRepository.GetAll();
            return Ok(wcData);
        }

        [HttpGet("/waterconsumption/topten")]
        public async Task<ActionResult<IEnumerable<WaterConsumption>>> GetTopTen()
        {
            var wcData = await _waterConsumptionRepository.GetTopTenConsumers();
            return Ok(wcData);
        }
    }
}
