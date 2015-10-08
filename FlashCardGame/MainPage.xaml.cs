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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlashCardGame
{
    public sealed partial class MainPage : Page
    {

        // Доступные названия картинок в директории Pics
        private string[] Names = { "Donut", "Coffee", "Hamburger", "Meat", "Pizza", "Salad", "Soup", "Sushi" };

        // Класс для хранения информации об одной ячейке доски
        private class Element
        {
            public BitmapImage Img { get; private set; }
            protected string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    Img = new BitmapImage(new Uri($"ms-appx:///Pics/{name}.png"));
                }
            }
            public bool IsOpen { get; set; } = false;
            public Button HostButton { get; set; }
            public Image HostImage { get; set; }
        }

        // Доска из 4x4 элементов
        private Element[,] Board = new Element[4,4];

        private Random Rnd = new Random();
        private BitmapImage ImgX = new BitmapImage(new Uri("ms-appx:///Pics/X.png"));

        // Фаза игры - открыта ли первая карточка (1) или пока нет (0)
        private int phase = 0;
        // Координаты карточки, открытой на предыдущем шаге
        private int prev_i, prev_j;

        public MainPage()
        {
            this.InitializeComponent();
            PopulateBoard();

            // Вычисляем размер отдельно взятой кнопки исходя из размеров окна
            var winSize = (int)Math.Min(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
            var sz = (winSize - 20) / 4;

            // Генерация UI поля 
            for (int i=0;i<4;i++)
            {
                // Формируем элемент вида <Stackpanel><Button><Image/></Button>...</Stackpanel>
                var s = new StackPanel();
                s.Orientation = Orientation.Horizontal;
                for (int j=0;j<4;j++)
                {
                    var img = new Image();
                    img.Height = img.Width = sz-20;
                    img.Source = ImgX;
                    var b = new Button();
                    b.HorizontalContentAlignment = HorizontalAlignment.Center;
                    b.VerticalContentAlignment = VerticalAlignment.Center;
                    b.Height = b.Width = sz;
                    b.Click += ClickHandler;
                    b.Content = img;
                    b.Tag = i * 10 + j;
                    s.Children.Add(b);
                    Board[i, j].HostButton = b;
                    Board[i, j].HostImage = img;
                }
                main.Children.Add(s);
            }
        }

        // Выбираем координаты еще не заполненной клетки поля
        private void Pick(ref int i, ref int j)
        {
            do
            {
                i = Rnd.Next(0, 4);
                j = Rnd.Next(0, 4);
            } while (Board[i, j] != null);
        }

        private void PopulateBoard()
        {
            int i = 0, j = 0;
            for (int k = 0; k < 8; k++)
            {
                Pick(ref i, ref j);
                Board[i, j] = new Element() { Name = Names[k] };
                Pick(ref i, ref j);
                Board[i, j] = new Element() { Name = Names[k] };
            }
        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            var btn = ((Button)sender);
            var i = (int)btn.Tag / 10;
            var j = (int)btn.Tag % 10;
            if (Board[i, j].IsOpen) return;
            if (phase == 0)
            {
                // Фаза 0: Открываем клетку и запоминаем координаты
                Board[i,j].HostImage.Source = Board[i, j].Img;
                prev_i = i; prev_j = j;
                phase++;
            }
            else
            {
                // Фаза 1: Проверяем на совпадение
                if (prev_i == i && prev_j == j) return;
                if (Board[prev_i,prev_j].Name == Board[i,j].Name)
                {
                    // карточки совпали - открываем вторую клетку
                    Board[i, j].HostImage.Source = Board[i, j].Img;
                    Board[i, j].IsOpen = Board[prev_i, prev_j].IsOpen = true;
                    Board[i, j].HostButton.IsEnabled = Board[prev_i, prev_j].HostButton.IsEnabled = false;
                }
                else
                {
                    // карточки не совпали - закрываем предыдущую клетку
                    Board[prev_i, prev_j].HostImage.Source = ImgX;
                    // TODO: Добавить анимацию, чтобы только что нажатая клетка была не короткий момент видна
                }
                phase = 0;
            }
        }
    }
}
