using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy
{
    public static class Utils
    {

        public static bool TryLoadRom(string path, out byte[] rom)
        {
            rom = null;

            try
            {
                if(!File.Exists(path)) 
                {
                    Console.WriteLine("File not found.");
                    return false;
                }             
                rom = File.ReadAllBytes(path);
                Console.WriteLine($"Loading {Path.GetFileName(path)}. {rom.Length} bytes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }







    }
}
