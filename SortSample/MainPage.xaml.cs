using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace SortSample
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<int> Data { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            Data = new ObservableCollection<int>();
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Data.Add(rnd.Next(1, 600));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = this;
            Sort();
        }

        private async void Sort()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                var x = Data[i];
                var k = i;
                for (int j = i + 1; j < Data.Count; j++)
                {
                    if (Data[j] < Data[k])
                    {
                        k = j;
                    }
                }
                Data[i] = Data[k];
                Data[k] = x;
                await Task.Delay(1000);
            }

        }
    }
}
