using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OpenWeatherMap
{

    public class WeatherRecord
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public DateTime When { get; set; }
        public BitmapImage Icon { get; set; }

        public string Date
        {
            get { return $"{When.Day:D2}.{When.Month:D2}"; }
        }
        
        public string FullDate
        {
            get { return When.ToString("D"); }
        }

    }

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public string AppID = "2de143494c0b295cca9337e1e96b00e0";
        // public string AppID = "<insert your OpenWeatherMap AppID Here>";

        public List<WeatherRecord> Forecast { get; set; } = new List<WeatherRecord>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cli = new HttpClient();
            var res = await cli.GetStringAsync("http://api.openweathermap.org/data/2.5/weather?q=Moscow&mode=json&units=metric&APPID="+AppID);
            dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(res);
            Temp.Text = x.main.temp.ToString();
            BitmapImage img = new BitmapImage(new Uri($"http://openweathermap.org/img/w/{x.weather[0].icon}.png"));
            Img.Source = img;

            res = await cli.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast/daily?q=Moscow&mode=json&units=metric&cnt=7&APPID="+AppID);
            x = Newtonsoft.Json.JsonConvert.DeserializeObject(res);
            foreach (var z in x.list)
            {
                Forecast.Add(new WeatherRecord()
                {
                    When = Convert((long)z.dt),
                    Temp = z.temp.day,
                    Pressure = z.pressure,
                    Humidity = z.humidity,
                    Icon = new BitmapImage(new Uri($"http://openweathermap.org/img/w/{z.weather[0].icon}.png"))
                });
            }
            Fore.ItemsSource = Forecast;
        }

        private DateTime Convert(long x)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(x).ToLocalTime();
            return dtDateTime;
        }

        private void Fore_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(WeatherDetail), e.ClickedItem);
        }
    }

}
