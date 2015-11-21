using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeatherStationPi2.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherStationPi2
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        BMP180 sensor = new BMP180();
        DispatcherTimer dt = new DispatcherTimer() { Interval = new TimeSpan(1000) };

        List<int> History = new List<int>();
        int count = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await sensor.Init();
            dt.Tick += (s,ea) =>
            {
                var x = sensor.Temperature;
                TheTextBlock.Text = $"{x.ToString()}C";
                if (count == 0)
                {
                    History.Add((int)((x-23) * 40));
                    if (History.Count > 10) History.RemoveAt(0);
                    Graph.ItemsSource = null;
                    Graph.ItemsSource = History;
                    count = 5;
                }
                else count--;
            };
            dt.Start();
        }

    }
}
