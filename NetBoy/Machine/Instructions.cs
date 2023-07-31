using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBoy.Machine
{
    public class Instructions
    {

        public Dictionary<byte, Instruction> InstructionsSet = new Dictionary<byte, Instruction>()
        {
            {0x00, new Instruction(){ InType = InType.IN_NOP, Mode = AddMode.AM_IMP } },
            {0x01, new Instruction(){ InType = InType.IN_LD, Mode = AddMode.AM_MR_SHORT} }



        };
        public struct Instruction
        {
            public InType InType;
            public AddMode Mode;
            public RegType RegType1;
            public RegType RegType2;
            public Cond Con;
            public byte Param;
            public int Cycles;
        }
        public enum AddMode
        {
            AM_R_SHORT,
            AM_MR_SHORT,
            AM_R_R,
            AM_MR_R,
            AM_R,
            AM_R_BYTE,
            AM_R_MR,
            AM_R_HLI,
            AM_R_HLD,
            AM_HLI_R,
            AM_HLD_R,
            AM_R_A8,
            AM_A8_R,
            AM_HL_SPR,
            AM_SHORT,
            AM_BYTE,
            AM_IMP,
            AM_SHORT_R,
            AM_MR_D8,
            AM_MR,
            AM_A16_R,
            AM_R_A16
        }

        public enum RegType
        {
            RT_NONE,
            RT_A,
            RT_F,
            RT_B,       
            RT_C,
            RT_D,
            RT_E,
            RT_H,
            RT_L,
            RT_AF,
            RT_BC,
            RT_DE,
            RT_HL,
            RT_SP,
            RT_PC
        }

        public enum Cond
        {
            CT_NONE,
            CT_NZ,
            CT_Z,
            CT_NC,
            CT_C
        }

       public enum InType
        {
            IN_NONE,
            IN_NOP, 
            IN_HALT,
            IN_STOP,
            IN_DI,
            IN_EI,
            IN_CCF,
            IN_SCF,
            IN_JP,
            IN_CALL,
            IN_RET,
            IN_RETI,
            IN_RST,
            IN_JR,
            IN_LD,
            IN_LDI,
            IN_LDD,
            IN_PUSH,
            IN_POP,
            IN_ADD, 
            IN_SUB, 
            IN_ADC, 
            IN_SBC, 
            IN_AND, 
            IN_XOR, 
            IN_OR,  
            IN_CP,  
            IN_INC, 
            IN_DEC, 
            IN_DAA, 
            IN_CPL, 
            IN_RLCA,
            IN_RLA,
            IN_RRCA,
            IN_RRA,
            IN_RLC,
            IN_RL,
            IN_RRC,
            IN_RR,
            IN_SLA,
            IN_SWAP,
            IN_SRA,
            IN_SRL,
            IN_BIT,
            IN_SET,
            IN_RES
        }

                
     

    }
}
