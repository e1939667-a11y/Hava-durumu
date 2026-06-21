using System;
using System.Windows.Media;
using System.IO;
using Newtonsoft.Json;

namespace HavaDurumu.Models
{
    public class AppSettings
    {
        [JsonIgnore]
        private static string _configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "HavaDurumu",
            "config.json"
        );

        public string ThemeColorHex { get; set; } = "#2F2F2F";
        public double BackgroundOpacity { get; set; } = 0.95;
        public int ViewMode { get; set; } = 1;
        public string ApiKey { get; set; } = "YOUR_API_KEY";
        public string DefaultCity { get; set; } = "Istanbul";

        [JsonIgnore]
        public Color ThemeColor
        {
            get => (Color)ColorConverter.ConvertFromString(ThemeColorHex);
            set => ThemeColorHex = value.ToString();
        }

        public void Save()
        {
            try
            {
                var dir = Path.GetDirectoryName(_configPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(_configPath, json);
            }
            catch { }
        }

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    var json = File.ReadAllText(_configPath);
                    return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch { }
            return new AppSettings();
        }
    }
}
