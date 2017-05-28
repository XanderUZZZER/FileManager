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

        public ConsoleForm(int w, int h)
        {
            width = w;
            height = h;
            Console.Title = "File manager";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            leftPanel = new Panel(0, 0, width / 2 - 2, height - 2);
            rightPanel = new Panel( width / 2 + 1, 0, width / 2 - 2, height - 2);
        }
    }
}
