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
            leftPanel = new Panel(1, width / 2, height);
            rightPanel = new Panel(1, width / 2, height);
        }
    }
}
