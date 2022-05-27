namespace WaterWatch.Models
{
    public class WaterConsumption
    {
        public int Id { get; set; }
        public string Neighbourhood { get; set; }

        public string SuburbGroup { get; set; }

        public int AverageMonthlyKL { get; set; }
        public string Coordinates { get; set; }
    }
}
