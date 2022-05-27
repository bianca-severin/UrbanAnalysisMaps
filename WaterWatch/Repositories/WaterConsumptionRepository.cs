using WaterWatch.Models;
using WaterWatch.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            //check if the data exists when application starts
            SaveData();
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

        public void SaveData()
        {
            //Check if table is empty before we load the data - else, skip the extract, transform and load process
            var res_dataset = _context.Consumptions.ToList();

            if(res_dataset.Count() ==0)
            {
                Console.WriteLine("No data");
                var geoJson = File.ReadAllText("D:\\C#\\C# Project\\WaterWatch\\WaterWatch\\WaterWatch\\Files\\water_consumption.geojson");
                dynamic jsonObj = JsonConvert.DeserializeObject(geoJson);

                foreach(var feature in jsonObj["features"])
                {
                    //Extract values from the file object using the fields
                    string str_neighbourhood = feature["properties"]["neighbourhood"];
                    string str_suburb_group = feature["properties"]["suburb_group"];
                    string str_avgMonthlyKL = feature["properties"]["averageMonthlyKL"];
                    string str_geometry = feature["geometry"]["coordinates"].ToString(Newtonsoft.Json.Formatting.None);

                    //Apply Transformations

                    //Remove .0's from values
                    string conv_avgMothlyKl = str_avgMonthlyKL.Replace(".0", "");

                    //Convert string to int
                    int avgMthlyKl = Convert.ToInt32(conv_avgMothlyKl);

                    //Load the Data in our table
                    //insance of our model
                    WaterConsumption waterConsumption = new WaterConsumption()
                    {
                        Neighbourhood = str_neighbourhood,
                        SuburbGroup = str_suburb_group,
                        AverageMonthlyKL = avgMthlyKl,
                        Coordinates = str_geometry,
                    };

                    _context.Consumptions.Add(waterConsumption);
                    //apply the changes to the table
                    _context.SaveChanges();

                }
            }
            else
            {
                Console.WriteLine("Data Loaded");
            }
        }
    }
}
