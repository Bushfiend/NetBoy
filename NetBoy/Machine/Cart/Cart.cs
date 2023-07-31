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
                Console.WriteLine($"Rom Loaded Successfully. {rom.Length}\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }


        private CartHeader ParseRomData(byte[] romData)
        {
            RomSize = (uint)romData.Length;

            byte[] entry = new byte[4]; // entry point to main program on cart
            Array.Copy(romData, 0x100, entry, 0, 4);

            byte[] logo = new byte[48]; // Nintendo Logo Data
            Array.Copy(romData, 0x104, logo, 0, 48);

            byte[] title = new byte[16]; // These bytes contain the title of the game in upper case ASCII
            Array.Copy(romData, 0x134, title, 0, 16);

            byte[] manufacturerCode = new byte[4]; // Manufacturers Code.
            Array.Copy(romData, 0x13F, manufacturerCode, 0, 4);

            byte[] cgbFlag = new byte[1]; // CGB Flag. 
            Array.Copy(romData, 0x143, cgbFlag, 0, 1);

            byte[] newLicensee = new byte[2]; // This area contains a two-character ASCII “licensee code” indicating the game’s publisher.
            Array.Copy(romData, 0x144, newLicensee, 0, 2);

            byte[] oldLicensee = new byte[1]; // This byte is used in older (pre-SGB) cartridges to specify the game’s publisher. 
            Array.Copy(romData, 0x14B, oldLicensee, 0, 1);

            byte[] sgbFlag = new byte[1]; // This byte specifies whether the game supports SGB functions. 
            Array.Copy(romData, 0x146, sgbFlag, 0, 1);

            byte[] cartType = new byte[1]; // This byte indicates what kind of hardware is present on the cartridge
            Array.Copy(romData, 0x147, cartType, 0, 1);

            byte[] romSize = new byte[1]; // This byte indicates how much ROM is present on the cartridge.
            Array.Copy(romData, 0x148, romSize, 0, 1);

            byte[] ramSize = new byte[1]; // This byte indicates how much RAM is present on the cartridge, if any.
            Array.Copy(romData, 0x149, ramSize, 0, 1);

            byte[] regionCode = new byte[1]; // This byte specifies whether this version of the game is intended to be sold.
            Array.Copy(romData, 0x14A, regionCode, 0, 1);

            byte[] romVersion = new byte[1]; // This byte specifies the version number of the game. It is usually $00.
            Array.Copy(romData, 0x14C, romVersion, 0, 1);

            byte[] checksum = new byte[1]; // This byte contains an 8-bit checksum computed from the cartridge header bytes
            Array.Copy(romData, 0x14D, checksum, 0, 1);

            byte[] globalChecksum = new byte[2]; // These bytes contain a 16-bit (big-endian) checksum.
            Array.Copy(romData, 0x14D, globalChecksum, 0, 1);

            CartHeader header = new CartHeader()
            {
                Entry = entry,
                Logo = logo,
                Title = title,
                ManufacturerCode = manufacturerCode,
                CGBFlag = cgbFlag[0],
                NewLicenseeCode = newLicensee,
                OldLicenseeCode = oldLicensee,
                SGBFlag = sgbFlag[0],
                CartType = cartType[0],
                RomSize = romSize[0],
                RamSize = ramSize[0],
                RegionCode = regionCode[0],
                RomVersion = romVersion[0],
                Checksum = checksum[0],
                GlobalChecksum = (ushort)((globalChecksum[0] << 8) | globalChecksum[1])
            };

            return header;
        }



        public void PrintCartInfo(CartHeader header)
        {
            StringBuilder sb = new StringBuilder();

            string titleName = Utils.ConvertToTitleCase(Encoding.ASCII.GetString(header.Title));
            string publisher = string.Empty;
            string cartType = CartDefinitions.GetCartrigeType(header.CartType);
            string romSize = CartDefinitions.GetRomSize(header.RomSize);
            string ramSize = CartDefinitions.GetRamSize(header.RamSize);
            string region = CartDefinitions.GetRegion(header.RegionCode);

            if (header.OldLicenseeCode[0] == 0x33)
            {
                publisher = CartDefinitions.GetNewCompany(Encoding.ASCII.GetString(header.NewLicenseeCode));
            }
            else
            {
                publisher = CartDefinitions.GetOldCompany(header.OldLicenseeCode[0]);
            }

            sb.AppendLine($"Title: {titleName}");
            sb.AppendLine($"Publisher: {publisher}");
            sb.AppendLine($"Cartridge Type: {cartType}");
            sb.AppendLine($"Rom Size: {romSize}");
            sb.AppendLine($"Ram Size: {ramSize}");
            sb.AppendLine($"Region: {region}");

            Console.WriteLine(sb.ToString());
        }


        public struct CartHeader
        {
            public byte[] Entry { get; set; }
            public byte[] Logo { get; set; }
            public byte[] Title { get; set; }
            public byte[] ManufacturerCode { get; set; }
            public byte SGBFlag { get; set; }
            public byte CGBFlag { get; set; }
            public byte[] NewLicenseeCode { get; set; }
            public byte[] OldLicenseeCode { get; set; }
            public byte CartType { get; set; }
            public byte RomSize { get; set; }
            public byte RamSize { get; set; }
            public byte RegionCode { get; set; }
            public byte RomVersion { get; set; }
            public byte Checksum { get; set; }
            public ushort GlobalChecksum { get; set; }
        }

    }
}
