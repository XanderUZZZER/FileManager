using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File manager");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();

            Console.SetWindowSize(40, 31);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            DrawBorder(10, 10, 20, 30);

            Console.ReadLine();
        }
        private static void WriteCharacterStrings(int start, int end,
                                             bool changeColor)
        {
            for (int ctr = start; ctr <= end; ctr++)
            {
                if (changeColor)
                    Console.BackgroundColor = (ConsoleColor)((ctr - 1) % 16);

                Console.WriteLine(new String(Convert.ToChar(ctr + 64), 30));
            }
        }
        static void DrawBorder(int x, int y, int w, int h)
        {
            // Draw top line
            Console.SetCursorPosition(x, y);
            Console.Write("╔");
            for (int i = x + 1; i < w - 1; i++)
            {
                Console.Write("═");
                Thread.Sleep(50);
            }
            Console.WriteLine("╗");

            // Draw Bottom line
            Console.SetCursorPosition(x, h);
            Console.Write("╚");
            for (int i = x + 1; i < w - 1; i++)
            {
                Console.Write("═");
                Thread.Sleep(50);
            }
            Console.WriteLine("╝");

            // Draw left line
            for (int i = y + 1; i < h; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.WriteLine("║");
                Thread.Sleep(50);
            }

            // Draw left line
            for (int i = y + 1; i < h; i++)
            {
                Console.SetCursorPosition(w - 1, i);
                Console.WriteLine("║");
                Thread.Sleep(50);
            }

            Console.SetCursorPosition(x + 1, y + 1);
            Console.WriteLine("test");
            Console.WriteLine("test");
        }
    }
}
