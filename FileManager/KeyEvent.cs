using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class MyEventArgs : EventArgs
    {
        public ConsoleKey ck;
    }

    class KeyEvent
    {
        public event EventHandler<MyEventArgs> KeyDown;

        public void OnKeyDown(ConsoleKey ck)
        {
            MyEventArgs args = new MyEventArgs();
            if (KeyDown != null)
            {
                args.ck = ck;
                KeyDown(this, args);
            }
        }
    }
}
