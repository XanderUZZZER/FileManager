using System;
using System.Collections.Generic;
using System.IO;
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
        Panel activePanel;
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
            leftPanel = new Panel(0, 0, width / 2 - 2, height - 2);
            rightPanel = new Panel( width / 2 + 1, 0, width / 2 - 2, height - 2);
            leftPanel.setActive();
            rightPanel.setUnactive();
            activePanel = leftPanel;
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
                            activePanel.MoveUpActiveItem();
                            break;

                        }
                    case ConsoleKey.DownArrow:
                        {
                            activePanel.MoveDownActiveItem();
                            break;
                        }
                    case ConsoleKey.Tab:
                        {
                            if (activePanel == leftPanel)
                            {
                                rightPanel.setActive();
                                leftPanel.setUnactive();
                                activePanel = rightPanel;
                            }                                
                            else
                            {
                                leftPanel.setActive();
                                rightPanel.setUnactive();
                                activePanel = leftPanel;
                            }
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            activePanel.OpenDir();
                            break;
                        }
                    case ConsoleKey.Backspace:
                        {
                            activePanel.CloseDir();
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
                Console.SetCursorPosition(64, 20);
                Console.CursorVisible = false;
                ConsoleKeyInfo cki;
                
                cki = Console.ReadKey();
                ck = cki.Key;
                evnt.OnKeyDown(cki.Key);
            }
        }
    }
}
