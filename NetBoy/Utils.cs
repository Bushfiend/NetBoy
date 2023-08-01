using NetBoy.Machine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static NetBoy.Machine.Instructions;

namespace NetBoy
{
    public static class Utils
    {

        public static string ConvertToTitleCase(string input)
        {
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(words[i]))
                    continue;

                string firstLetter = words[i].Substring(0, 1).ToUpper(CultureInfo.InvariantCulture);
                string restOfWord = words[i].Substring(1).ToLower(CultureInfo.InvariantCulture);
                words[i] = firstLetter + restOfWord;
            }

            return string.Join(" ", words);
        }


        public static void InvalidOp(byte code)
        {
            Console.WriteLine($"Invalid Opcode ( 0x{code.ToString("X2")} )");
        }

        public static void NotImpl(byte code)
        {
            Console.WriteLine($"Instruction (0x{code.ToString("X2")}) Not Implemented ");
        }

        public static void InInfo(byte code, Instructions.Instruction instruction)
        {
            Console.WriteLine($"Instruction Type: {instruction.Type} - (0x{code.ToString("X2")}). ");


        }


    }
}
