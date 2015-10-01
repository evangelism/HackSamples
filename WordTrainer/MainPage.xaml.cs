using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WordTrainer.DataModel;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordTrainer
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Word SelectedWord { get; set; }
        public Word[] Words { get; set; }
        public Word TheWord { get; set; }
        int Correct, Total;

        void NewWord()
        {
            if (SelectedWord!= null)
            {
                if (SelectedWord.Name == TheWord.Name) Correct++;
                Total++;
            }
            Words = Word.GetWordSample("*", 4);
            TheWord = Helpers.Pick(Words);
            tbWord.Text = TheWord.Name;
            if (Total>0) tbResult.Text = string.Format("Correct={0}, Total={1}, Percent={2}%", Correct, Total, 100 * Correct / Total);
            gvPics.SelectedItem = null;
            gvPics.ItemsSource = Words;
        }

        private void gvPics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = gvPics.SelectedItem as Word;
            NewWord();
        }

        public MainPage()
        {
            this.InitializeComponent();
            NewWord();
        }
    }
}
