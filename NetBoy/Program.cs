using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetBoy
{
    internal class Program
    {
        static string path = "C:\\Roms\\Pokemon Blue.gb";
        const string title = "NetBoy";

        Window window = null;
        static void Main(string[] args)
        {

            Window window = new Window();

            while (true)
            {
                byte[] rom;
                Utils.TryLoadRom(path, out rom);

                window.Init(title, 500, 500);


                Thread.Sleep(2000000);
            }

            



        }
    }
}
