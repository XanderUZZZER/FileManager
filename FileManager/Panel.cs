using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Panel
    {
        int top;
        int left;
        int width;
        int height;
        int cursorOffset = 1;
        int activeItemIndex = 0;
        ConsoleColor foregroundColor;
        ConsoleColor backgroundColor;
        DirectoryInfo currentDir;
        DirectoryInfo[] dirs;
        FileInfo[] files;
        List<FileSystemInfo> items = new List<FileSystemInfo>();

        public Panel(int left, int top, int w, int h, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {            
            this.left = left;
            this.top = top;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            width = w;
            height = h;
            currentDir = new DirectoryInfo("C:\\");
            //currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            dirs = currentDir.GetDirectories();
            files = currentDir.GetFiles();
            items.AddRange(dirs);
            items.AddRange(files);
            drawBorder();
            drawAllItems();
        }

        void drawBorder()
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;      
            Console.SetCursorPosition(left, top);
            Console.Write("╔");
            for (int i = 1; i < width; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            for (int i = top + 1; i<height; i++)
            {
                Console.SetCursorPosition(left, i);
                Console.Write("║");
                Console.SetCursorPosition(left + width, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(left, height);
            Console.Write("╚");
            for (int i =1 ; i < width; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");                      
        }
        void drawAllItems()
        {
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write($"{currentDir.Root}");

            Console.SetCursorPosition(left + 1, top + 2);
            for (int i = left + 1; i < left + width; i++)
            {
                Console.Write("═");
            }

            cursorOffset = 3;
            foreach (var item in items)
            {
                Console.SetCursorPosition(left + 1, top + cursorOffset);
                Console.Write($"{item.Name,-62}");
                cursorOffset++;
            }
            drawActiveItem();
        }
        void drawActiveItem()
        {
            Console.SetCursorPosition(left + 1, top + activeItemIndex + 3);
            Console.BackgroundColor = foregroundColor;
            Console.ForegroundColor = backgroundColor;
            Console.Write($"{items[activeItemIndex].Name, -62}");
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }
        public void MoveUpActiveItem()
        {
            if (activeItemIndex != 0)
                activeItemIndex--;
            drawAllItems();
        }
        public void MoveDownActiveItem()
        {
            if (activeItemIndex != items.Count - 1)
                activeItemIndex++;
            drawAllItems();
        }
    }
}
