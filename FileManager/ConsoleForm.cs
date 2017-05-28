using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class ConsoleForm
    {
        int width;
        int height;
        Panel leftPanel;
        Panel rightPanel;
        KeyEvent evnt = new KeyEvent();
        ConsoleKey ck;     

        public ConsoleForm(int w, int h)
        {
            width = w;
            height = h;
            Console.Title = "File manager";
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.CursorVisible = false;
            leftPanel = new Panel(0, 0, width / 2 - 2, height - 2, ConsoleColor.White, ConsoleColor.DarkGray);
            rightPanel = new Panel( width / 2 + 1, 0, width / 2 - 2, height - 2, ConsoleColor.Gray, ConsoleColor.DarkGray);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, height - 1);
            string s = "F1 - copy | F2 - cut | F3 - paste | F4 - root | F5 - list of disks | F6 - properties | F7 - rename | F8 - find | F9 - new folder ";
            Console.Write($"{s, -1}");
        }
        public void Run()
        {
            evnt.KeyDown += (sender, e) =>
            {
                switch (e.ck)
                {
                    case ConsoleKey.UpArrow:
                        {
                            leftPanel.MoveUpActiveItem();
                            break;

                        }
                    case ConsoleKey.DownArrow:
                        {
                            leftPanel.MoveDownActiveItem();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            };
            while (true)
            {
                Console.SetCursorPosition(20, 20);
                Console.CursorVisible = false;
                ConsoleKeyInfo cki;
                
                cki = Console.ReadKey();
                ck = cki.Key;
                evnt.OnKeyDown(cki.Key);
            }
        }
    }
}
