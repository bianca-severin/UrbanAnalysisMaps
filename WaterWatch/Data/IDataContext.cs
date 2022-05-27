using Microsoft.EntityFrameworkCore;
using WaterWatch.Models;

namespace WaterWatch.Data
{
    public interface IDataContext
    {
        DbSet<WaterConsumption> Consumptions { get; set; }

        int SaveChanges();
    }
}
