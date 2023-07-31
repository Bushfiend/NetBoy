using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Machine
{
    public class Cart
    {
        public byte[] RomData;
        public uint RomSize;
        public CartHeader Header;



        public Cart(string path)
        {
            if(!TryLoadRom(path, out RomData))
            {
                return;
            }

            Header = ParseRomData(RomData);

            PrintCartInfo(Header);
        }


       
        private CartHeader ParseRomData(byte[] romData)
        {
            RomSize = (uint)romData.Length;

            byte[] entry = new byte[4]; // entry point to main program on cart
            Array.Copy(romData, 0x100, entry, 0, 4);

            byte[] logo = new byte[48]; // Nintendo Logo Data
            Array.Copy(romData, 0x104, logo, 0, 48);

            byte[] title = new byte[16]; // Title of game. In Ascii
            Array.Copy(romData, 0x134, title, 0, 16);





            CartHeader header = new CartHeader()
            {
                Entry = entry,
                Logo = logo,
                Title = title

            };

            return header;
        }
      
        private void PrintCartInfo(CartHeader header)
        {
            StringBuilder sb = new StringBuilder();
    
            var titleName = Utils.ConvertToTitleCase(Encoding.ASCII.GetString(header.Title));


            sb.Append($"Title: {titleName}");
            sb.AppendLine();



            Console.WriteLine(sb.ToString());

        }

        private bool TryLoadRom(string path, out byte[] rom)
        {
            rom = null;

            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File not found.");
                    return false;
                }
                rom = File.ReadAllBytes(path);
                Console.WriteLine($"Found {Path.GetFileName(path)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }




        public struct CartHeader
        {
            public byte[] Entry { get; set; }
            public byte[] Logo { get; set; }
            public byte[] Title { get; set; }
            public byte[] MFGCode { get; set; }
            public byte SgbFlag { get; set; }
            public byte[] NewLicCode { get; set; }
            public byte[] OldLicCode { get; set; }
            public byte Type { get; set; }
            public byte RomSize { get; set; }
            public byte RamSize { get; set; }
            public byte DesinationCode { get; set; }
            public byte RomVersion { get; set; }
            public byte[] Checksum { get; set; }
            public ushort[] GlobalChecksum { get; set; }
        }

    }
}
