using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmoRecognizer
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static string OxfordAPIKey = "2cabd9f1b2014a04bc04782b3c703539";

        MediaCapture MC;
        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
        EmotionServiceClient Oxford = new EmotionServiceClient(OxfordAPIKey);

        EmoCollection MyEmo = new EmoCollection();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await Init();
            dt.Tick += GetEmotions;
            dt.Start();
        }

        private async Task Init()
        {
            MC = new MediaCapture();
            var cameras = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            var camera = cameras.First();
            var settings = new MediaCaptureInitializationSettings() { VideoDeviceId = camera.Id };
            await MC.InitializeAsync(settings);
            ViewFinder.Source = MC;
            await MC.StartPreviewAsync();
        }

        async void GetEmotions(object sender, object e)
        {
            // dt.Stop();
            var ms = new MemoryStream();
            await MC.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), ms.AsRandomAccessStream());
            ms.Position = 0L;
            var Emo = await Oxford.RecognizeAsync(ms);
            if (Emo!=null && Emo.Length>0)
            {
                var Face = Emo[0];
                var s = Face.Scores;
                // res.Text = $"Happiness: {s.Happiness,6:N4}\nAnger: {s.Anger,6:N4}\nContempt: {s.Contempt,6:N4}\nDisgust: {s.Disgust,6:N4}\nFear: {s.Fear,6:N4}\nSadness: {s.Sadness,6:N4}\nSurprise: {s.Surprise,6:N4}";
                // Canvas.SetLeft(res, Face.FaceRectangle.Left+Face.FaceRectangle.Width/2);
                // Canvas.SetTop(res, Face.FaceRectangle.Top+Face.FaceRectangle.Height/2);
                var T = new Thickness();
                T.Left = Face.FaceRectangle.Left;
                T.Top=Face.FaceRectangle.Top;
                // res.Margin = T;
                EmoControl.Margin = T;
                MyEmo.Update(Face.Scores);
            }
        }


    }

}
