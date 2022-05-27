using Microsoft.EntityFrameworkCore;
using WaterWatch.Models;

namespace WaterWatch.Data
{
    public class DataContext: DbContext, IDataContext
    {

        //Constructor
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<WaterConsumption> Consumptions { get; set; }
    }
}
