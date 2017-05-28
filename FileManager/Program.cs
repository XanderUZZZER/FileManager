using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File manager");
            ConsoleForm form = new ConsoleForm(Console.WindowWidth, Console.WindowHeight);
            Console.ReadLine();
        }
    }
}
