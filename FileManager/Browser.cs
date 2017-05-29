using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class Browser
    {
        DirectoryInfo currentDir;
        FileInfo[] files;
        DirectoryInfo[] dirs;

        public Browser()
        {
            currentDir = new DirectoryInfo(@"C:\");
            GetCurrentItems();
        }

        public void GetCurrentItems()
        {
            dirs = currentDir.GetDirectories();
            files = currentDir.GetFiles();
            ShowContent();
        }

        public void ShowContent()
        {
            //Console.Clear();
            foreach (var dir in dirs)
            {
                Console.WriteLine(dir.Name + "\t\t<dir>");
            }
                
            foreach (var file in files)
            {
                Console.WriteLine(file.Name + $"\t\t<{file.Extension}>");
            }
                
            Console.WriteLine();
        }

        public void OpenFolder(string dirName)
        {            
            currentDir = new DirectoryInfo (currentDir.Name + dirName);
            GetCurrentItems();
        }
        public void CloseFolder()
        {
            currentDir = currentDir.Parent;
            GetCurrentItems();
        }

        public void SetActiveItem()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}
