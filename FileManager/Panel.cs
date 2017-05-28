using System;
using System.Collections.Generic;
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

        public Panel(int left, int top, int w, int h)
        {            
            this.left = left;
            this.top = top;
            width = w;
            height = h;
            draw();
        }

        void draw()
        {            
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
    }
}
