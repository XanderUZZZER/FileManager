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
            Console.SetWindowSize(130, 30);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            ConsoleForm form = new ConsoleForm(Console.WindowWidth , Console.WindowHeight );
            form.Run();
            Console.ReadLine();
        }
    }
}
