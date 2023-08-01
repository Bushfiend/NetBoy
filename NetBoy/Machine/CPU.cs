using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Machine
{
    public class CPU
    {
        public Registers Register;

        private Cart Cartridge;
        private Bus Bus;

        private int EmuCycles;

        public byte CurrentOpcode;

        public CPU(Cart cart)
        {
            Cartridge = cart;
            Register = new Registers(cart.Header.Entry[0]);
            Bus = new Bus(cart);
            Register.PC = 0x100;
            EmuCycles = 0;

        }
        //50 CE 66
        public void Tick()
        {
            CurrentOpcode = Bus.Read(Register.PC);

            Decode(CurrentOpcode);

            Register.PC++;
        }



        private void Decode(byte opcode)
        {
            if(!Instructions.Set.ContainsKey(opcode)) { Utils.InvalidOp(opcode); return; }



            var instruction = Instructions.Set[opcode];


            if(instruction.InstType == Instructions.InType.None)
            {
                Utils.NotImpl(opcode);
                Register.PC--;
            }
            else
            {
                Utils.InInfo(opcode, instruction);
            }

        }


        public class Registers
        {
            public byte A, F, B, C, D, E, H, L;

            public ushort AF { get => GetAF(); set => SetAF(value); }
            public ushort BC { get => GetBC(); set => SetBC(value); }
            public ushort DE { get => GetDE(); set => SetDE(value); }
            public ushort HL { get => GetHL(); set => SetHL(value); }

            public ushort PC;
            public ushort SP;

            public Registers(ushort entry)
            {
                PC = entry;
                SP = 0;
                A = 0;
                F = 0;
                B = 0;
                C = 0;
                D = 0;
                E = 0;
                H = 0;
                L = 0;
            }

                  
            public ushort GetAF()
            {
                return (ushort)((A << 8) | F);
            }
            public void SetAF(ushort value)
            {
                A = (byte)(value >> 8);
                F = (byte)(value & 0xFF);
            }

            public ushort GetBC()
            {
                return (ushort)((B << 8) | C);
            }
            public void SetBC(ushort value)
            {
                B = (byte)(value >> 8);
                C = (byte)(value & 0xFF);
            }

            public ushort GetDE()
            {
                return (ushort)((D << 8) | E);
            }
            public void SetDE(ushort value)
            {
                D = (byte)(value >> 8);
                E = (byte)(value & 0xFF);
            }

            public ushort GetHL()
            {
                return (ushort)((H << 8) | L);
            }
            public void SetHL(ushort value)
            {
                H = (byte)(value >> 8);
                L = (byte)(value & 0xFF);
            }

        }


        //example.. maybe
        // A = 0xFF or 11111111
        // F = 0x0F or 00001111
        // A << 8 shifting the bits left
        // A = 0xFF00 or 11111111 00000000
        // Then bitwise Or is performed shifting A and F
        // the result is 0xFF0F or 11111111 00001111.. i hope

        // Bitwise OR.  If either value is 1 then the result is 1 otherwise 0
        // Bitwise AND. If both values are 1 then the result is 1 otherwise 0
        // Bitwise NOT. If a value is 1 then the result is 0. If it is 0, the result is 1.
        // Bitwise XOR. Compares two values, if both values are the same is the result is 0. else the value is 1.
    }
}
