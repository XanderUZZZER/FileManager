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
        ConsoleColor foregroundColor = ConsoleColor.White;
        ConsoleColor backgroundColor = ConsoleColor.DarkGray;
        public DirectoryInfo currentDir;
        DirectoryInfo[] dirs;
        FileInfo[] files;
        List<FileSystemInfo> items = new List<FileSystemInfo>();
        public FileSystemInfo ActiveItem;
        int activeItemIndex = 0;

        public Panel(int left, int top, int width, int height)
        {            
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            currentDir = new DirectoryInfo("C:\\");
            dirs = currentDir.GetDirectories();
            files = currentDir.GetFiles();
            items.AddRange(dirs);
            items.AddRange(files);
            ActiveItem = items[activeItemIndex];
            DrawBorder();
            drawAllItems();
        }

        void DrawBorder()
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
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
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
                if (cursorOffset == 29)
                    cursorOffset = 3;
                cursorOffset++;
            }
            for (int i = cursorOffset; i< height; i++)
            {
                Console.SetCursorPosition(left + 1, top + i);
                Console.Write($"{"", -62}");
            }
            drawActiveItem();
        }
        void drawActiveItem()
        {
            Console.SetCursorPosition(left + 1, top + activeItemIndex + 3);
            Console.BackgroundColor = foregroundColor;
            Console.ForegroundColor = backgroundColor;
            Console.Write($"{ActiveItem.Name,-62}");
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }
        public void MoveUpActiveItem()
        {
            if (activeItemIndex != 0)
            {
                activeItemIndex--;
                ActiveItem = items[activeItemIndex];
            }                
            drawAllItems();
        }
        public void MoveDownActiveItem()
        {
            if (activeItemIndex != items.Count - 1 && items.Count != 0)
            {
                activeItemIndex++;
                ActiveItem = items[activeItemIndex];
            }                
            drawAllItems();
        }
        public void setActive()
        {
            foregroundColor = ConsoleColor.White;
            DrawBorder();
            drawAllItems();
        }
        public void setUnactive()
        {
            foregroundColor = ConsoleColor.Gray;
            DrawBorder();
            drawAllItems();
        }
        public void OpenDir()
        {
            if (ActiveItem is DirectoryInfo)
                currentDir = new DirectoryInfo(ActiveItem.FullName);
            dirs = currentDir.GetDirectories();
            files = currentDir.GetFiles();
            items.Clear();
            items.AddRange(dirs);
            items.AddRange(files);
            activeItemIndex = 0;
            if (items.Count > 0)
                ActiveItem = items[activeItemIndex];
            DrawBorder();
            drawAllItems();
        }
        public void CloseDir()
        {
            if (currentDir.Parent.Name != null)
                currentDir = new DirectoryInfo(currentDir.Parent.Name);
            dirs = currentDir.GetDirectories();
            files = currentDir.GetFiles();
            items.Clear();
            items.AddRange(dirs);
            items.AddRange(files);
            activeItemIndex = 0;
            ActiveItem = items[activeItemIndex];
            DrawBorder();
            drawAllItems();
        }
    }
}
