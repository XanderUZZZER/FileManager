using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Panel
    {
        int xStart;
        int width;
        int height;

        public Panel(int x, int w, int h)
        {
            xStart = x;            
            width = w;
            height = h;
            draw();
        }

        void draw()
        {
            Console.WriteLine("╔");
            Console.WriteLine("╗");
            Console.WriteLine("╚");
            Console.WriteLine("╝");
        }
    }
}
