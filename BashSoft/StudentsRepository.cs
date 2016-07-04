using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;

        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData()
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData();
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitialisedException);
            }
        }

        private static void ReadData()
        {
            string input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                string[] tokens = input.Split(' ');
                string course = tokens[0];
                string students = tokens[1];
                int mark = int.Parse(tokens[2]);

                //TODO: Add the course and the student if they don't exist
                //TODO: Add the mark
            }
        }
    }
}
