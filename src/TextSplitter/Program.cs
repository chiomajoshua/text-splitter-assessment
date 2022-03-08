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

            var splitText = SplitText(filePath, limit);           
            Console.WriteLine(string.Join(Environment.NewLine, splitText));

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Please provide path to save text");
            var writeFilePath = Console.ReadLine();

            WriteFile(splitText, writeFilePath);

            Console.ReadKey();
        }

        /// <summary>
        /// Splits text into different lines using the line limitter
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static IEnumerable<string> SplitText(string filePath, int limit)
        {

            if (string.IsNullOrEmpty(filePath))
                throw new Exception("File Path Is Not Part of Request.");

            if (!IsTextFile(filePath))
                throw new Exception("File Is Not A Text File.");

            if (!IsFileExist(filePath))
                throw new Exception("File Path Is Not Valid");


            var text = ReadFile(filePath);

            if(string.IsNullOrEmpty(text))
                throw new Exception("No Text In File.");

            for (int i = 0; i < text.Length; i += limit)
                yield return text.Substring(i, Math.Min(limit, text.Length - i)).Replace("\n", "").Replace("\r", "");
        }


        /// <summary>
        /// Checks if The File Exists
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsFileExist(string filePath)
        {
            return File.Exists($@"{filePath}");
        }

        /// <summary>
        /// Validate if file has a .txt extension
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsTextFile(string filePath)
        {
            if (filePath.Contains(".txt"))
                return true;
            return false;
        }


        /// <summary>
        /// Reads Content From File
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadFile(string filePath)
        {
            string line;
            try
            {
                StreamReader streamReader = new($@"{filePath}");
                line = streamReader.ReadToEndAsync().Result;
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return string.Empty;
            }
            return line;
        }


        /// <summary>
        /// Writes text to file
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static void WriteFile(IEnumerable<string> textToWrite, string filePath)
        {
            try
            {
                if (!IsFileExist(filePath))
                    throw new Exception("File Path Is Not Valid");

                StreamWriter sw = new($@"{filePath}");
                                

                foreach (var item in textToWrite)
                {
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                        continue;

                    sw.WriteLine(item);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }        
    }
}