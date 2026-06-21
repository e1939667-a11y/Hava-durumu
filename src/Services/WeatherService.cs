using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HavaDurumu.Models;

namespace HavaDurumu.Services
{
    public class WeatherService
    {
        private const string ApiBaseUrl = "https://api.openweathermap.org/data/2.5";
        private string _apiKey = "YOUR_API_KEY";
        private static HttpClient _httpClient = new HttpClient();

        public async Task<Weather> GetCurrentWeatherAsync(string city)
        {
            try
            {
                var url = $"{ApiBaseUrl}/weather?q={city}&appid={_apiKey}&units=metric&lang=tr";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                return new Weather
                {
                    City = city,
                    Temperature = Math.Round((double)json["main"]["temp"], 1),
                    Condition = (string)json["weather"][0]["main"],
                    Humidity = (int)json["main"]["humidity"],
                    WindSpeed = Math.Round((double)json["wind"]["speed"], 1),
                    IconUrl = $"https://openweathermap.org/img/wn/{json["weather"][0]["icon"]}@2x.png"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
                return null;
            }
        }

        public async Task<DailyForecast[]> GetFiveDayForecastAsync(string city)
        {
            try
            {
                var url = $"{ApiBaseUrl}/forecast?q={city}&appid={_apiKey}&units=metric&lang=tr";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                var forecasts = new List<DailyForecast>();
                int count = 0;
                for (int i = 0; i < json["list"].Count() && count < 5; i += 8)
                {
                    var item = json["list"][i];
                    var timestamp = UnixTimeStampToDateTime((long)item["dt"]);
                    
                    forecasts.Add(new DailyForecast
                    {
                        Date = timestamp.ToString("ddd, dd MMM"),
                        MaxTemp = Math.Round((double)item["main"]["temp_max"], 1),
                        MinTemp = Math.Round((double)item["main"]["temp_min"], 1),
                        Condition = (string)item["weather"][0]["main"],
                        IconUrl = $"https://openweathermap.org/img/wn/{item["weather"][0]["icon"]}@2x.png"
                    });
                    count++;
                }
                return forecasts.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
                return new DailyForecast[0];
            }
        }

        public string GetMoonPhase()
        {
            var today = DateTime.Now;
            var knownNewMoon = new DateTime(2000, 1, 6);
            var daySinceNewMoon = (today - knownNewMoon).Days % 29;

            return daySinceNewMoon switch
            {
                >= 0 and < 2 => "🌑 Yeni Ay",
                >= 2 and < 9 => "🌒 İlk Çeyrek",
                >= 9 and < 11 => "🌓 Büyüyen Yarım",
                >= 11 and < 16 => "🌔 Dolunay Yaklaşıyor",
                >= 16 and < 18 => "🌕 Dolunay",
                >= 18 and < 23 => "🌖 Azalan Yarım",
                >= 23 and < 27 => "🌗 Son Çeyrek",
                >= 27 => "🌘 Eski Ay",
                _ => "🌙 Bilinmiyor"
            };
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
