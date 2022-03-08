using System;
using System.Collections.Generic;
using System.IO;

namespace TextSplitter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please provide file path:");
            var filePath = Console.ReadLine();

            Console.WriteLine("Please provide line limit");
            var limit = Convert.ToInt32(Console.ReadLine());

            var splitText = Core.SplitText(filePath, limit);           
            Console.WriteLine(string.Join(Environment.NewLine, splitText));

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Please provide path to save text");
            var writeFilePath = Console.ReadLine();

            Core.WriteFile(splitText, writeFilePath);

            Console.ReadKey();
        }       
    }
}