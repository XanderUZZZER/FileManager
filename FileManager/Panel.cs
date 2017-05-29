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
        DirectoryInfo currentDir;
        List<FileSystemInfo> items = new List<FileSystemInfo>();
        FileSystemInfo ActiveItem;
        int activeItemIndex = 0;
        int startDispalyIndex = 0;
        int endDispalyIndex = 0;
        Stack<int> tempIndex = new Stack<int>();
        DriveInfo[] allDrives = DriveInfo.GetDrives();

        public Panel(int left, int top, int width, int height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            currentDir = new DirectoryInfo("C:\\");
            GetItems();
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
            for (int i = top + 1; i < height; i++)
            {
                Console.SetCursorPosition(left, i);
                Console.Write("║");
                Console.SetCursorPosition(left + width, i);
                Console.Write("║");
            }
            Console.SetCursorPosition(left, height);
            Console.Write("╚");
            for (int i = 1; i < width; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
        }
        void DrawAllItems()
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write($"{currentDir.FullName,-62}");
            Console.SetCursorPosition(left + 1, top + 2);
            for (int i = left + 1; i < left + width; i++)
            {
                Console.Write("═");
            }

            cursorOffset = 3;
            for (int i = 0; i < height && i + startDispalyIndex <= endDispalyIndex && items.Count != 0; i++)
            {
                Console.SetCursorPosition(left + 1, top + cursorOffset);
                Console.Write($"{items[i + startDispalyIndex].Name,-62}");
                cursorOffset++;
            }
            for (int i = cursorOffset; i < height; i++)
            {
                Console.SetCursorPosition(left + 1, top + i);
                Console.Write($"{"",-62}");
            }
            DrawActiveItem();
        }
        void DrawActiveItem()
        {
            if (items.Count != 0)
            {
                Console.SetCursorPosition(left + 1, top + activeItemIndex - startDispalyIndex + 3);
                Console.BackgroundColor = foregroundColor;
                Console.ForegroundColor = backgroundColor;
                Console.Write($"{ActiveItem.Name,-62}");
                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;
            }
        }
        void DrawError(string err)
        {
            Console.SetCursorPosition(left + 25, top / 2);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{err,10}");
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }
        string DrawInput()
        {
            Console.SetCursorPosition(left + 25, top / 2);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Input name:");
            string s = Console.ReadLine();
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            return s;
        }
        public void MoveUpActiveItem()
        {
            if (items.Count != 0 && activeItemIndex != startDispalyIndex && activeItemIndex != 0)
            {
                activeItemIndex--;
                ActiveItem = items[activeItemIndex];
            }
            else if (activeItemIndex == startDispalyIndex && activeItemIndex != 0)
            {
                activeItemIndex--;
                ActiveItem = items[activeItemIndex];
                startDispalyIndex = activeItemIndex;
                endDispalyIndex = items.Count - startDispalyIndex > 24 ? startDispalyIndex + 24 : items.Count - 1;
            }
            DrawBorder();
            DrawAllItems();
        }
        public void MoveDownActiveItem()
        {
            if (items.Count != 0 && activeItemIndex != endDispalyIndex)
            {
                activeItemIndex++;
                ActiveItem = items[activeItemIndex];
            }
            else if (activeItemIndex == endDispalyIndex && activeItemIndex != items.Count - 1)
            {
                activeItemIndex++;
                ActiveItem = items[activeItemIndex];
                startDispalyIndex++;
                endDispalyIndex = items.Count - startDispalyIndex > 24 ? endDispalyIndex + 1 : items.Count - 1;
            }
            DrawBorder();
            DrawAllItems();
        }
        public void SetActive()
        {
            foregroundColor = ConsoleColor.White;
            DrawBorder();
            DrawAllItems();
        }
        public void SetUnactive()
        {
            foregroundColor = ConsoleColor.Gray;
            DrawBorder();
            DrawAllItems();
        }
        public void Root()
        {
            tempIndex.Clear();
            currentDir = new DirectoryInfo(currentDir.Root.Name);
            GetItems();
            DrawBorder();
            DrawAllItems();
        }
        public void OpenDir()
        {
            if (ActiveItem is DirectoryInfo)
            {
                tempIndex.Push(activeItemIndex);
                currentDir = new DirectoryInfo(ActiveItem.FullName);
                try
                {
                    GetItems();
                    DrawBorder();
                    DrawAllItems();
                }
                catch (UnauthorizedAccessException)
                {
                    CloseDir();
                    DrawBorder();
                    DrawAllItems();
                    DrawError("Access denied");
                }
            }
        }
        public void CloseDir()
        {
            if (currentDir.Name != currentDir.Root.Name)
            {
                currentDir = new DirectoryInfo(currentDir.Parent.FullName);
                GetItems();
                if (tempIndex.Count != 0)
                    activeItemIndex = tempIndex.Pop();
                ActiveItem = items[activeItemIndex];
            }
            DrawBorder();
            DrawAllItems();
        }
        public void CreateDir()
        {
            string input = DrawInput();
            try
            {
                currentDir.CreateSubdirectory(input);
                GetItems();
                DrawBorder();
                DrawAllItems();
            }
            catch
            {
                GetItems();
                DrawBorder();
                DrawAllItems();
                DrawError("Wrong name, can't create folder");
            }
        }
        public void Rename()
        {
            string input = DrawInput();
            try
            {
                if (ActiveItem is FileInfo)
                {
                    File.Copy(ActiveItem.FullName, currentDir.FullName + "\\" + input);
                    File.Delete(ActiveItem.FullName);
                }
                else
                {
                    Directory.Move(ActiveItem.FullName, currentDir.FullName + "\\" + input);
                }
                GetItems();
                DrawBorder();
                DrawAllItems();
            }
            catch
            {
                GetItems();
                DrawBorder();
                DrawAllItems();
                DrawError("Can't rename");
            }
        }
        public void ShowDisks()
        {
            items.Clear();
            activeItemIndex = 0;
            foreach (var item in allDrives)
            {
                items.Add(item.RootDirectory);
            }
            ActiveItem = items[activeItemIndex];
            endDispalyIndex = items.Count - 1;
            DrawAllItems();
        }
        void GetItems()
        {
            items.Clear();
            items.AddRange(currentDir.GetDirectories());
            items.AddRange(currentDir.GetFiles());
            activeItemIndex = 0;
            if (items.Count > 0)
            {
                ActiveItem = items[activeItemIndex];
                startDispalyIndex = 0;
                endDispalyIndex = items.Count > 24 ? 24 : items.Count - 1;
            }
        }
    }
}
