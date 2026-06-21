using System;

namespace HavaDurumu.Models
{
    public class Weather
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string IconUrl { get; set; }
    }

    public class HourlyForecast
    {
        public string Time { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public string IconUrl { get; set; }
    }

    public class DailyForecast
    {
        public string Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string Condition { get; set; }
        public string IconUrl { get; set; }
    }
}
