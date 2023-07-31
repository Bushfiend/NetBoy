using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public static void NotImp(string something = "null")
        {
            Console.WriteLine($"{something} Not Implemented");
        }



    }
}
