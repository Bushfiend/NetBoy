using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Machine
{
    public static class Instructions
    {

        private static Instruction CurrentInstruction;
        private static CPU cpu;


        private static ushort GetA16()
        {
            return (ushort)(((cpu.Cartridge.RomData[cpu.Register.PC + 1]) << 8) | (cpu.Cartridge.RomData[cpu.Register.PC + 2]));
        }

        private static void NOP()
        {

        }


        private static void Jump()
        {
            switch(CurrentInstruction.Opcode)
            {
                case 0xC3:
                    var a16 = GetA16();
                    cpu.Register.PC = a16;
                    break;

            }
        }

        private static void Load()
        {
            switch (CurrentInstruction.Opcode)
            {
                case 0x01:
                    var a16 = GetA16();
                    cpu.Register.BC = a16;
                    break;

            }

        }
        private static void Dec()
        {
            switch (CurrentInstruction.Opcode)
            {
                case 0x05:
                    cpu.Register.B--;
                    break;
                case 0x15:
                    cpu.Register.D--;
                    break;
                case 0x25:
                    cpu.Register.H--;
                    break;
                case 0x35:
                    cpu.Register.HL--;
                    break;

                case 0x0D:
                    cpu.Register.C--;
                    break;
                case 0x1D:
                    cpu.Register.E--;
                    break;
                case 0x2D:
                    cpu.Register.L--;
                    break;
                case 0x3D:
                    cpu.Register.A--;
                    break;
            }

        }

        private static void Inc()
        {
            switch (CurrentInstruction.Opcode)
            {
                case 0x04:
                    cpu.Register.B++;
                    break;
                case 0x14:
                    cpu.Register.D++;
                    break;
                case 0x24:
                    cpu.Register.H++;
                    break;
                case 0x34:
                    cpu.Register.HL++;
                    break;
               
                case 0x0C:
                    cpu.Register.C++;
                    break;
                case 0x1C:
                    cpu.Register.E++;
                    break;
                case 0x2C:
                    cpu.Register.L++;
                    break;
                case 0x3C:
                    cpu.Register.A++;
                    break;           
            }

        }


        private static Dictionary<InstructionType, Action> SortedInstuctions = new Dictionary<InstructionType, Action>()
        {
            {InstructionType.Jump, Jump},
            {InstructionType.Inc, Inc},


        };




        public static void Execute(Instruction Instruction, CPU c)
        {
            if (!SortedInstuctions.ContainsKey(Instruction.Type))
                return;
            if (cpu == null)
                cpu = c;

            CurrentInstruction = Instruction;

            var implementation = SortedInstuctions[CurrentInstruction.Type];

            implementation.Invoke();

        }


        public static Dictionary<byte, Instruction> Set = new Dictionary<byte, Instruction>()
        {
            {0x00, new Instruction(){ Type = InstructionType.Nop, Cycles = 1} },
            {0x01, new Instruction(){ Type = InstructionType.Load, Cycles = 3} },
            {0x02, new Instruction(){Type = InstructionType.None }},
            {0x03, new Instruction(){Type = InstructionType.None }},
            {0x04, new Instruction(){Type = InstructionType.Inc, Cycles = 1}},
            {0x05, new Instruction(){Type = InstructionType.Dec, Cycles = 1}},
            {0x06, new Instruction(){Type = InstructionType.None }},
            {0x07, new Instruction(){Type = InstructionType.None }},
            {0x08, new Instruction(){Type = InstructionType.None }},
            {0x09, new Instruction(){Type = InstructionType.None }},
            {0x0A, new Instruction(){Type = InstructionType.None }},
            {0x0B, new Instruction(){Type = InstructionType.None }},
            {0x0C, new Instruction(){Type = InstructionType.None }},
            {0x0D, new Instruction(){Type = InstructionType.None }},
            {0x0E, new Instruction(){Type = InstructionType.None }},
            {0x0F, new Instruction(){Type = InstructionType.None }},
            {0x10, new Instruction(){Type = InstructionType.None }},
            {0x11, new Instruction(){Type = InstructionType.None }},
            {0x12, new Instruction(){Type = InstructionType.None }},
            {0x13, new Instruction(){Type = InstructionType.None }},
            {0x14, new Instruction(){Type = InstructionType.Inc, Cycles = 1 }},
            {0x15, new Instruction(){Type = InstructionType.Inc, Cycles = 1 }},
            {0x16, new Instruction(){Type = InstructionType.None }},
            {0x17, new Instruction(){Type = InstructionType.None }},
            {0x18, new Instruction(){Type = InstructionType.None }},
            {0x19, new Instruction(){Type = InstructionType.None }},
            {0x1A, new Instruction(){Type = InstructionType.None }},
            {0x1B, new Instruction(){Type = InstructionType.None }},
            {0x1C, new Instruction(){Type = InstructionType.None }},
            {0x1D, new Instruction(){Type = InstructionType.None }},
            {0x1E, new Instruction(){Type = InstructionType.None }},
            {0x1F, new Instruction(){Type = InstructionType.None }},
            {0x20, new Instruction(){Type = InstructionType.None }},
            {0x21, new Instruction(){Type = InstructionType.None }},
            {0x22, new Instruction(){Type = InstructionType.None }},
            {0x23, new Instruction(){Type = InstructionType.None }},
            {0x24, new Instruction(){Type = InstructionType.Inc, Cycles = 1 }},
            {0x25, new Instruction(){Type = InstructionType.Dec, Cycles = 1 }},
            {0x26, new Instruction(){Type = InstructionType.None }},
            {0x27, new Instruction(){Type = InstructionType.None }},
            {0x28, new Instruction(){Type = InstructionType.None }},
            {0x29, new Instruction(){Type = InstructionType.None }},
            {0x2A, new Instruction(){Type = InstructionType.None }},
            {0x2B, new Instruction(){Type = InstructionType.None }},
            {0x2C, new Instruction(){Type = InstructionType.Inc, Cycles = 1 }},
            {0x2D, new Instruction(){Type = InstructionType.None }},
            {0x2E, new Instruction(){Type = InstructionType.None }},
            {0x2F, new Instruction(){Type = InstructionType.None }},
            {0x30, new Instruction(){Type = InstructionType.None }},
            {0x31, new Instruction(){Type = InstructionType.None }},
            {0x32, new Instruction(){Type = InstructionType.None }},
            {0x33, new Instruction(){Type = InstructionType.None }},
            {0x34, new Instruction(){Type = InstructionType.Inc, Cycles = 3 }},
            {0x35, new Instruction(){Type = InstructionType.Dec, Cycles = 3 }},
            {0x36, new Instruction(){Type = InstructionType.None }},
            {0x37, new Instruction(){Type = InstructionType.None }},
            {0x38, new Instruction(){Type = InstructionType.None }},
            {0x39, new Instruction(){Type = InstructionType.None }},
            {0x3A, new Instruction(){Type = InstructionType.None }},
            {0x3B, new Instruction(){Type = InstructionType.None }},
            {0x3C, new Instruction(){Type = InstructionType.None }},
            {0x3D, new Instruction(){Type = InstructionType.None }},
            {0x3E, new Instruction(){Type = InstructionType.None }},
            {0x3F, new Instruction(){Type = InstructionType.None }},
            {0x40, new Instruction(){Type = InstructionType.None }},
            {0x41, new Instruction(){Type = InstructionType.None }},
            {0x42, new Instruction(){Type = InstructionType.None }},
            {0x43, new Instruction(){Type = InstructionType.None }},
            {0x44, new Instruction(){Type = InstructionType.None }},
            {0x45, new Instruction(){Type = InstructionType.None }},
            {0x46, new Instruction(){Type = InstructionType.None }},
            {0x47, new Instruction(){Type = InstructionType.None }},
            {0x48, new Instruction(){Type = InstructionType.None }},
            {0x49, new Instruction(){Type = InstructionType.None }},
            {0x4A, new Instruction(){Type = InstructionType.None }},
            {0x4B, new Instruction(){Type = InstructionType.None }},
            {0x4C, new Instruction(){Type = InstructionType.None }},
            {0x4D, new Instruction(){Type = InstructionType.None }},
            {0x4E, new Instruction(){Type = InstructionType.None }},
            {0x4F, new Instruction(){Type = InstructionType.None }},
            {0x50, new Instruction(){Type = InstructionType.None }},
            {0x51, new Instruction(){Type = InstructionType.None }},
            {0x52, new Instruction(){Type = InstructionType.None }},
            {0x53, new Instruction(){Type = InstructionType.None }},
            {0x54, new Instruction(){Type = InstructionType.None }},
            {0x55, new Instruction(){Type = InstructionType.None }},
            {0x56, new Instruction(){Type = InstructionType.None }},
            {0x57, new Instruction(){Type = InstructionType.None }},
            {0x58, new Instruction(){Type = InstructionType.None }},
            {0x59, new Instruction(){Type = InstructionType.None }},
            {0x5A, new Instruction(){Type = InstructionType.None }},
            {0x5B, new Instruction(){Type = InstructionType.None }},
            {0x5C, new Instruction(){Type = InstructionType.None }},
            {0x5D, new Instruction(){Type = InstructionType.None }},
            {0x5E, new Instruction(){Type = InstructionType.None }},
            {0x5F, new Instruction(){Type = InstructionType.None }},
            {0x60, new Instruction(){Type = InstructionType.None }},
            {0x61, new Instruction(){Type = InstructionType.None }},
            {0x62, new Instruction(){Type = InstructionType.None }},
            {0x63, new Instruction(){Type = InstructionType.None }},
            {0x64, new Instruction(){Type = InstructionType.None }},
            {0x65, new Instruction(){Type = InstructionType.None }},
            {0x66, new Instruction(){Type = InstructionType.None }},
            {0x67, new Instruction(){Type = InstructionType.None }},
            {0x68, new Instruction(){Type = InstructionType.None }},
            {0x69, new Instruction(){Type = InstructionType.None }},
            {0x6A, new Instruction(){Type = InstructionType.None }},
            {0x6B, new Instruction(){Type = InstructionType.None }},
            {0x6C, new Instruction(){Type = InstructionType.None }},
            {0x6D, new Instruction(){Type = InstructionType.None }},
            {0x6E, new Instruction(){Type = InstructionType.None }},
            {0x6F, new Instruction(){Type = InstructionType.None }},
            {0x70, new Instruction(){Type = InstructionType.None }},
            {0x71, new Instruction(){Type = InstructionType.None }},
            {0x72, new Instruction(){Type = InstructionType.None }},
            {0x73, new Instruction(){Type = InstructionType.None }},
            {0x74, new Instruction(){Type = InstructionType.None }},
            {0x75, new Instruction(){Type = InstructionType.None }},
            {0x76, new Instruction(){Type = InstructionType.None }},
            {0x77, new Instruction(){Type = InstructionType.None }},
            {0x78, new Instruction(){Type = InstructionType.None }},
            {0x79, new Instruction(){Type = InstructionType.None }},
            {0x7A, new Instruction(){Type = InstructionType.None }},
            {0x7B, new Instruction(){Type = InstructionType.None }},
            {0x7C, new Instruction(){Type = InstructionType.None }},
            {0x7D, new Instruction(){Type = InstructionType.None }},
            {0x7E, new Instruction(){Type = InstructionType.None }},
            {0x7F, new Instruction(){Type = InstructionType.None }},
            {0x80, new Instruction(){Type = InstructionType.None }},
            {0x81, new Instruction(){Type = InstructionType.None }},
            {0x82, new Instruction(){Type = InstructionType.None }},
            {0x83, new Instruction(){Type = InstructionType.None }},
            {0x84, new Instruction(){Type = InstructionType.None }},
            {0x85, new Instruction(){Type = InstructionType.None }},
            {0x86, new Instruction(){Type = InstructionType.None }},
            {0x87, new Instruction(){Type = InstructionType.None }},
            {0x88, new Instruction(){Type = InstructionType.None }},
            {0x89, new Instruction(){Type = InstructionType.None }},
            {0x8A, new Instruction(){Type = InstructionType.None }},
            {0x8B, new Instruction(){Type = InstructionType.None }},
            {0x8C, new Instruction(){Type = InstructionType.None }},
            {0x8D, new Instruction(){Type = InstructionType.None }},
            {0x8E, new Instruction(){Type = InstructionType.None }},
            {0x8F, new Instruction(){Type = InstructionType.None }},
            {0x90, new Instruction(){Type = InstructionType.None }},
            {0x91, new Instruction(){Type = InstructionType.None }},
            {0x92, new Instruction(){Type = InstructionType.None }},
            {0x93, new Instruction(){Type = InstructionType.None }},
            {0x94, new Instruction(){Type = InstructionType.None }},
            {0x95, new Instruction(){Type = InstructionType.None }},
            {0x96, new Instruction(){Type = InstructionType.None }},
            {0x97, new Instruction(){Type = InstructionType.None }},
            {0x98, new Instruction(){Type = InstructionType.None }},
            {0x99, new Instruction(){Type = InstructionType.None }},
            {0x9A, new Instruction(){Type = InstructionType.None }},
            {0x9B, new Instruction(){Type = InstructionType.None }},
            {0x9C, new Instruction(){Type = InstructionType.None }},
            {0x9D, new Instruction(){Type = InstructionType.None }},
            {0x9E, new Instruction(){Type = InstructionType.None }},
            {0x9F, new Instruction(){Type = InstructionType.None }},
            {0xA0, new Instruction(){Type = InstructionType.None }},
            {0xA1, new Instruction(){Type = InstructionType.None }},
            {0xA2, new Instruction(){Type = InstructionType.None }},
            {0xA3, new Instruction(){Type = InstructionType.None }},
            {0xA4, new Instruction(){Type = InstructionType.None }},
            {0xA5, new Instruction(){Type = InstructionType.None }},
            {0xA6, new Instruction(){Type = InstructionType.None }},
            {0xA7, new Instruction(){Type = InstructionType.None }},
            {0xA8, new Instruction(){Type = InstructionType.None }},
            {0xA9, new Instruction(){Type = InstructionType.None }},
            {0xAA, new Instruction(){Type = InstructionType.None }},
            {0xAB, new Instruction(){Type = InstructionType.None }},
            {0xAC, new Instruction(){Type = InstructionType.None }},
            {0xAD, new Instruction(){Type = InstructionType.None }},
            {0xAE, new Instruction(){Type = InstructionType.None }},
            {0xAF, new Instruction(){Type = InstructionType.None }},
            {0xB0, new Instruction(){Type = InstructionType.None }},
            {0xB1, new Instruction(){Type = InstructionType.None }},
            {0xB2, new Instruction(){Type = InstructionType.None }},
            {0xB3, new Instruction(){Type = InstructionType.None }},
            {0xB4, new Instruction(){Type = InstructionType.None }},
            {0xB5, new Instruction(){Type = InstructionType.None }},
            {0xB6, new Instruction(){Type = InstructionType.None }},
            {0xB7, new Instruction(){Type = InstructionType.None }},
            {0xB8, new Instruction(){Type = InstructionType.None }},
            {0xB9, new Instruction(){Type = InstructionType.None }},
            {0xBA, new Instruction(){Type = InstructionType.None }},
            {0xBB, new Instruction(){Type = InstructionType.None }},
            {0xBC, new Instruction(){Type = InstructionType.None }},
            {0xBD, new Instruction(){Type = InstructionType.None }},
            {0xBE, new Instruction(){Type = InstructionType.None }},
            {0xBF, new Instruction(){Type = InstructionType.None }},
            {0xC0, new Instruction(){Type = InstructionType.None }},
            {0xC1, new Instruction(){Type = InstructionType.None }},
            {0xC2, new Instruction(){Type = InstructionType.None }},
            {0xC3, new Instruction(){Type = InstructionType.Jump }},
            {0xC4, new Instruction(){Type = InstructionType.None }},
            {0xC5, new Instruction(){Type = InstructionType.None }},
            {0xC6, new Instruction(){Type = InstructionType.None }},
            {0xC7, new Instruction(){Type = InstructionType.None }},
            {0xC8, new Instruction(){Type = InstructionType.None }},
            {0xC9, new Instruction(){Type = InstructionType.None }},
            {0xCA, new Instruction(){Type = InstructionType.None }},                   
            {0xCD, new Instruction(){Type = InstructionType.None }},
            {0xCE, new Instruction(){Type = InstructionType.None }},
            {0xCF, new Instruction(){Type = InstructionType.None }},
            {0xD0, new Instruction(){Type = InstructionType.None }},
            {0xD1, new Instruction(){Type = InstructionType.None }},
            {0xD2, new Instruction(){Type = InstructionType.None }},
            {0xD3, new Instruction(){Type = InstructionType.None }},
            {0xD5, new Instruction(){Type = InstructionType.None }},
            {0xD6, new Instruction(){Type = InstructionType.None }},
            {0xD7, new Instruction(){Type = InstructionType.None }},
            {0xD8, new Instruction(){Type = InstructionType.None }},
            {0xD9, new Instruction(){Type = InstructionType.None }},
            {0xDA, new Instruction(){Type = InstructionType.None }},          
            {0xDC, new Instruction(){Type = InstructionType.None }},            
            {0xDE, new Instruction(){Type = InstructionType.None }},
            {0xDF, new Instruction(){Type = InstructionType.None }},
            {0xE0, new Instruction(){Type = InstructionType.None }},
            {0xE1, new Instruction(){Type = InstructionType.None }},
            {0xE2, new Instruction(){Type = InstructionType.None }},                    
            {0xE5, new Instruction(){Type = InstructionType.None }},
            {0xE6, new Instruction(){Type = InstructionType.None }},
            {0xE7, new Instruction(){Type = InstructionType.None }},
            {0xE8, new Instruction(){Type = InstructionType.None }},
            {0xE9, new Instruction(){Type = InstructionType.None }},
            {0xEA, new Instruction(){Type = InstructionType.None }},          
            {0xEE, new Instruction(){Type = InstructionType.None }},
            {0xEF, new Instruction(){Type = InstructionType.None }},
            {0xF0, new Instruction(){Type = InstructionType.None }},
            {0xF1, new Instruction(){Type = InstructionType.None }},
            {0xF2, new Instruction(){Type = InstructionType.None }},
            {0xF3, new Instruction(){Type = InstructionType.None }},          
            {0xF5, new Instruction(){Type = InstructionType.None }},
            {0xF6, new Instruction(){Type = InstructionType.None }},
            {0xF7, new Instruction(){Type = InstructionType.None }},
            {0xF8, new Instruction(){Type = InstructionType.None }},
            {0xF9, new Instruction(){Type = InstructionType.None }},
            {0xFA, new Instruction(){Type = InstructionType.None }},
            {0xFB, new Instruction(){Type = InstructionType.None }},          
            {0xFE, new Instruction(){Type = InstructionType.None }},
            {0xFF, new Instruction(){Type = InstructionType.None }},

        };
        public struct Instruction
        {
            public InstructionType Type;
            public byte Opcode;
            public int Cycles;
        }
       

       public enum InstructionType
        {
            ///<summary> Nothing or Not Implemented</summary>
            None,
            ///<summary> No Operation</summary>
            Nop,
            ///<summary> No Operation</summary>
            Halt,
            Stop,
            Load,
            Jump,
            ///<summary> Increment a Register</summary>
            Inc,
            ///<summary> Decrement a Register</summary>
            Dec

        }

                
     

    }
}
