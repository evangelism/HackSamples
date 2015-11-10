using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SexRecognizer
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        string OxfordAPIKey = "fbc8d74540864a5db8273fdb5c26eed8";

        FaceServiceClient OxFaceRecognizer;

        public MainPage()
        {
            this.InitializeComponent();
            OxFaceRecognizer = new FaceServiceClient(OxfordAPIKey);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null) { return; }

            var b = new BitmapImage(new Uri(photo.Path));
            img.Source = b;

            var imgStream = await photo.OpenAsync(FileAccessMode.Read);

            txt.Text = "Recognizing...";

            var faces = await OxFaceRecognizer.DetectAsync(imgStream.AsStream(), true, true, true, false);

            txt.Text = "Done";

            if (faces != null && faces.Length > 0)
            {
                var f = faces[0];
                txt.Text = $"{f.Attributes.Gender}, {f.Attributes.Age}";
                rect.Visibility = Visibility.Visible;
                Canvas.SetLeft(rect, f.FaceRectangle.Left);
                Canvas.SetTop(rect, f.FaceRectangle.Top);
                rect.Height = f.FaceRectangle.Height;
                rect.Width = f.FaceRectangle.Width;
            }
        }
    }
}
