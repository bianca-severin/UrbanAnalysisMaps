using WaterWatch.Models;

namespace WaterWatch.Repositories
{
    public interface IWaterConsumptionRepository
    {
        Task<IEnumerable<WaterConsumption>> GetAll();
        Task<IEnumerable<WaterConsumption>> GetTopTenConsumers();
    }
}
