using WaterWatch.Models;
using WaterWatch.Data;
using Microsoft.EntityFrameworkCore;

namespace WaterWatch.Repositories
{
    public class WaterConsumptionRepository: IWaterConsumptionRepository
    {
        private readonly IDataContext _context;

        //Constructor
        public WaterConsumptionRepository(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WaterConsumption>> GetAll()
        {
            //query to get our data and return it as a list
            return await _context.Consumptions.ToListAsync();
        }

        public async Task<IEnumerable<WaterConsumption>> GetTopTenConsumers()
        {
            var q = _context.Consumptions
               .OrderByDescending(avgKL => avgKL.AverageMonthlyKL) // order in descending order
               .Take(10) // select Top 10 records
               .ToListAsync(); // return the data as a list

            //return the data set
            return await q;
        }
    }
}
