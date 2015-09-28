using Microsoft.SmallBasic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleDrawings
{
    class Program
    {

        // Not very clever implementation of House
        // Изучаем простейшее рисование черепашкой
        public static void House_Primitive(int len)
        {
            Turtle.Speed = 9;

            // Рисуем тело домика
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);

            // Рисуем крышу
            Turtle.Turn(-60);
            Turtle.Move(len);
            Turtle.Turn(120);
            Turtle.Move(len);
            Turtle.Turn(30);

            // Рисуем окошко
            Turtle.PenUp();
            Turtle.Move(2 * len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.PenDown();
            Turtle.Move(len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.Turn(90);
        }

        // ФУНКЦИИ
        // Упрощаем рисование домика, вводя функцию для рисования квадрата
        static void Square(int len)
        {
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
            Turtle.Move(len);
            Turtle.Turn(90);
        }

        static void House(int len)
        {
            Turtle.Turn(90);
            Square(len);

            // Рисуем крышу
            Turtle.Turn(-60);
            Turtle.Move(len);
            Turtle.Turn(120);
            Turtle.Move(len);
            Turtle.Turn(30);

            // Рисуем окошко
            Turtle.PenUp();
            Turtle.Move(2 * len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.PenDown();
            Square(len / 3);
        }

        // Псевдослучайные числа. Рисуем домик из случайных цветов.

        static Random Rnd = new Random();

        static void Colorize()
        {
            var r = Rnd.Next(1, 10);
            if (r < 3) GraphicsWindow.PenColor = "Blue";
            else if (r < 5) GraphicsWindow.PenColor = "Red";
            else if (r < 7) GraphicsWindow.PenColor = "Yellow";
            else GraphicsWindow.PenColor = "Black";
        }

        static void CMove(int n)
        {
            Colorize();
            Turtle.Move(n);
        }

        static void CSquare(int len)
        {
            CMove(len);
            Turtle.Turn(90);
            CMove(len);
            Turtle.Turn(90);
            CMove(len);
            Turtle.Turn(90);
            CMove(len);
            Turtle.Turn(90);
        }

        static void ColorHouse(int len)
        {
            Turtle.Turn(90);
            CSquare(len);

            // Рисуем крышу
            Turtle.Turn(-60);
            CMove(len);
            Turtle.Turn(120);
            CMove(len);
            Turtle.Turn(30);

            // Рисуем окошко
            Turtle.PenUp();
            CMove(2 * len / 3);
            Turtle.Turn(90);
            CMove(len / 3);
            Turtle.PenDown();
            CSquare(len / 3);
        }


        // ЦИКЛЫ. Цикл While.

        // GoodHouse рисует домик и возвращает черепашку в правильное место
        static void GoodHouse(int len)
        {
            // Рисуем тело домика
            Turtle.Turn(90);
            Square(len);

            // Рисуем крышу
            Turtle.Turn(-60);
            Turtle.Move(len);
            Turtle.Turn(120);
            Turtle.Move(len);
            Turtle.Turn(30);

            // Рисуем окошко
            Turtle.PenUp();
            Turtle.Move(2 * len / 3);
            Turtle.Turn(90);
            Turtle.Move(len / 3);
            Turtle.PenDown();
            Square(len / 3);
            Turtle.PenUp();
            Turtle.Move(2 * len / 3);
            Turtle.TurnRight();
            Turtle.Move(2 * len / 3);
            Turtle.PenDown();
        }

        static void StreetWhile(int n)
        {
            var num = 0;
            while (num<n)
            {
                GoodHouse(50);
                Turtle.PenUp();
                Turtle.TurnRight();
                Turtle.Move(1.3 * 50);
                Turtle.TurnLeft();
                Turtle.PenDown();
                num++;
            }
        }

        static void Street(int n)
        {
            for(var num=0;num< n;num++)
            {
                GoodHouse(50);
                Turtle.PenUp();
                Turtle.TurnRight();
                Turtle.Move(1.3 * 50);
                Turtle.TurnLeft();
                Turtle.PenDown();
            }
        }

        static void City(int n, int m)
        {
            for (int j = 0; j < m; j++)
            {
                Street(n);
                Turtle.PenUp();
                Turtle.TurnLeft();
                Turtle.Move(1.3 * 50 * 4);
                Turtle.TurnLeft();
                Turtle.Move(100);
                Turtle.Turn(180);
                Turtle.PenDown();
            }
        }

        // Szierpinsky Triangle
        static void Szierpinsky()
        {
            GraphicsWindow.Show();
            int x = 100, y = 100;
            var Rnd = new Random();
            for (int i = 0; i < 100000; i++)
            {
                GraphicsWindow.SetPixel(x, y, "Black");
                var r = Rnd.Next(1, 30);
                if (r < 10) Move(ref x, ref y, 100, 500);
                else if (r < 20) Move(ref x, ref y, 600, 500);
                else Move(ref x, ref y, 350, 100);
            }
        }

        static void Move(ref int x, ref int y, int nx, int ny)
        {
            x = (x + nx) / 2;
            y = (y + ny) / 2;
        }


        // Koch Snowflake. Recursion.
        public static void Draw(int len, int n)
        {
            if (n == 0) Turtle.Move(len);
            else
            {
                Draw(len / 3, n - 1);
                Turtle.Turn(-60);
                Draw(len / 3, n - 1);
                Turtle.Turn(120);
                Draw(len / 3, n - 1);
                Turtle.Turn(-60);
                Draw(len / 3, n - 1);
            }
        }

        static void Koch(int n)
        {
            Turtle.Turn(30);
            Draw(200, n);
            Turtle.Turn(120);
            Draw(200, n);
            Turtle.Turn(120);
            Draw(200, n);
            Turtle.Turn(120);
        }

        // TODO: Move more samples here from "Inspirational C#"
        // TODO: Add Mandelbrot set drawing

        static void Main(string[] args)
        {
            Console.WriteLine(@"
Please select the drawing type:
1. House
2. Color House
3. Street
4. City
5. Serpinsky
6. Koch
");
            var x = Console.ReadKey().KeyChar;
            Turtle.Speed = 10;
            switch (x)
            {
                case '1':
                    House(100); break;
                case '2':
                    ColorHouse(100); break;
                case '3':
                    Street(5); break;
                case '4':
                    City(4,3); break;
                case '5':
                    Szierpinsky(); break;
                case '6':
                    Koch(4); break;

            }
        }
    }
}
