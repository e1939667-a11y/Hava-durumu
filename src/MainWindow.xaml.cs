using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using HavaDurumu.Services;
using HavaDurumu.Models;

namespace HavaDurumu
{
    public partial class MainWindow : Window
    {
        private WeatherService _weatherService;
        private AppSettings _settings;
        private ViewMode _currentViewMode = ViewMode.Medium;

        public enum ViewMode
        {
            Small,   // Sadece bugün
            Medium,  // Bugün + 5 gün
            Large    // Bugün + 5 gün + saatlik bar
        }

        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
            _settings = new AppSettings();
            LoadSettings();
            LoadWeatherData();
            this.MouseRightButtonUp += MainWindow_MouseRightButtonUp;
        }

        private async void LoadWeatherData()
        {
            try
            {
                // API'dan veri çekme
                var weather = await _weatherService.GetCurrentWeatherAsync(_settings.DefaultCity);
                if (weather != null)
                {
                    CurrentTemp.Text = $"{weather.Temperature}°C";
                    CurrentCondition.Text = weather.Condition;
                    CurrentDesc.Text = $"Nem: {weather.Humidity}%";
                    CurrentWind.Text = $"Rüzgar: {weather.WindSpeed} m/s";
                    MoonPhase.Text = _weatherService.GetMoonPhase();
                }

                // 5 günlük tahmin
                var forecast = await _weatherService.GetFiveDayForecastAsync(_settings.DefaultCity);
                if (forecast.Length > 0)
                {
                    FiveDaysList.ItemsSource = forecast;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void LoadSettings()
        {
            MainGrid.Background = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromArgb(
                    (byte)(_settings.BackgroundOpacity * 255),
                    _settings.ThemeColor.R,
                    _settings.ThemeColor.G,
                    _settings.ThemeColor.B
                )
            );
            SetViewMode((ViewMode)_settings.ViewMode);
        }

        private void MainWindow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var contextMenu = new System.Windows.Controls.ContextMenu();

            // Tema Rengi
            var themeColorItem = new System.Windows.Controls.MenuItem { Header = "🎨 Tema Rengi" };
            var colorOptions = new[]
            {
                ("Koyu Gri", System.Windows.Media.Colors.DarkGray),
                ("Lacivert", System.Windows.Media.Color.FromRgb(25, 55, 109)),
                ("Koyu Yeşil", System.Windows.Media.Color.FromRgb(34, 67, 55)),
                ("Mürekkep", System.Windows.Media.Color.FromRgb(31, 31, 31))
            };
            foreach (var (name, color) in colorOptions)
            {
                var item = new System.Windows.Controls.MenuItem { Header = name };
                item.Click += (s, args) => ChangeThemeColor(color);
                themeColorItem.Items.Add(item);
            }
            contextMenu.Items.Add(themeColorItem);

            // Şeffaflık
            var opacityItem = new System.Windows.Controls.MenuItem { Header = "👁️ Şeffaflık" };
            for (int i = 50; i <= 100; i += 10)
            {
                var item = new System.Windows.Controls.MenuItem { Header = $"{i}%" };
                item.Click += (s, args) => ChangeBackgroundOpacity(i / 100.0);
                opacityItem.Items.Add(item);
            }
            contextMenu.Items.Add(opacityItem);

            // Görünüm Modu
            var viewModeItem = new System.Windows.Controls.MenuItem { Header = "📺 Görünüm" };
            var smallView = new System.Windows.Controls.MenuItem { Header = "Küçük (Bugün)", Tag = ViewMode.Small };
            var mediumView = new System.Windows.Controls.MenuItem { Header = "Orta (Bugün + 5 Gün)", Tag = ViewMode.Medium };
            var largeView = new System.Windows.Controls.MenuItem { Header = "Büyük (Tam)", Tag = ViewMode.Large };
            smallView.Click += (s, args) => SetViewMode(ViewMode.Small);
            mediumView.Click += (s, args) => SetViewMode(ViewMode.Medium);
            largeView.Click += (s, args) => SetViewMode(ViewMode.Large);
            viewModeItem.Items.Add(smallView);
            viewModeItem.Items.Add(mediumView);
            viewModeItem.Items.Add(largeView);
            contextMenu.Items.Add(viewModeItem);

            // Kapat
            var closeItem = new System.Windows.Controls.MenuItem { Header = "❌ Kapat" };
            closeItem.Click += (s, args) => this.Close();
            contextMenu.Items.Add(closeItem);

            contextMenu.IsOpen = true;
        }

        private void ChangeThemeColor(System.Windows.Media.Color color)
        {
            _settings.ThemeColor = color;
            _settings.Save();
            LoadSettings();
        }

        private void ChangeBackgroundOpacity(double opacity)
        {
            _settings.BackgroundOpacity = opacity;
            _settings.Save();
            LoadSettings();
        }

        private void SetViewMode(ViewMode mode)
        {
            _currentViewMode = mode;
            _settings.ViewMode = (int)mode;
            _settings.Save();

            switch (mode)
            {
                case ViewMode.Small:
                    this.Height = 280;
                    FiveDayBorder.Visibility = Visibility.Collapsed;
                    HourlyBorder.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.Medium:
                    this.Height = 420;
                    FiveDayBorder.Visibility = Visibility.Visible;
                    HourlyBorder.Visibility = Visibility.Collapsed;
                    break;
                case ViewMode.Large:
                    this.Height = 600;
                    FiveDayBorder.Visibility = Visibility.Visible;
                    HourlyBorder.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
