using NetBoy.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Machine
{
    public class Bus
    {
        /*
            0000 - 3FFF	    16 KiB ROM bank 00	From cartridge, usually a fixed bank
            4000 - 7FFF	    16 KiB ROM Bank 01~NN	From cartridge, switchable bank via mapper (if any)
            8000 - 9FFF	    8 KiB Video RAM (VRAM)	In CGB mode, switchable bank 0/1
            A000 - BFFF	    8 KiB External RAM	From cartridge, switchable bank if any
            C000 - CFFF	    4 KiB Work RAM (WRAM)	
            D000 - DFFF	    4 KiB Work RAM (WRAM)	In CGB mode, switchable bank 1~7
            E000 - FDFF	    Mirror of C000~DDFF (ECHO RAM)	Nintendo says use of this area is prohibited.
            FE00 - FE9F	    Object attribute memory (OAM)	
            FEA0 - FEFF	    Not Usable	Nintendo says use of this area is prohibited
            FF00 - FF7F	    I/O Registers	
            FF80 - FFFE	    High RAM (HRAM)	
            FFFF - FFFF	    Interrupt Enable register (IE)	
        */

        private Cart Cartridge;


        public Bus(Cart cart)
        {
            Cartridge = cart;
        }



        public byte BusRead(ushort address)
        {
            if(address < 0x8000)
            {
                return Cartridge.RomData[address];
            }

            Utils.NotImp("Read < 0x8000");
            return 0;         
        }


        public void Write(ushort address, byte value)
        {
            if (address < 0x8000)
            {
                Cartridge.RomData[address] = value;
            }

            Utils.NotImp("Write < 0x8000");
        }




    }
}
