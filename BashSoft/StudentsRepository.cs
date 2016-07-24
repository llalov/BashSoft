using System;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;
        public static string initializedDataName = "";

        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData(string fileName)
        {
            if (initializedDataName != fileName)
            {
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitialisedException);
            }
        }

        private static void ReadData(string fileName)
        {
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            string currentPath = SessionData.currentPath;
            fileName = fileName.TrimStart('\\');
            currentPath += '\\' + fileName;
            currentPath = currentPath.TrimEnd('/', '\\');
            if (File.Exists(currentPath))
            {
                string[] input = System.IO.File.ReadAllLines(currentPath);
                for (int i = 0; i < input.Length; i++)
                {
                    string[] tokens = input[i].Split(' ');
                    string course = tokens[0];
                    string student = tokens[1];
                    int mark = int.Parse(tokens[2]);

                    if (!studentsByCourse.ContainsKey(course))
                    {
                        studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                    }

                    if (!studentsByCourse[course].ContainsKey(student))
                    {
                        studentsByCourse[course].Add(student, new List<int>());
                    }

                    studentsByCourse[course][student].Add(mark);
                }
                initializedDataName = fileName;
                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            return false;
        }

        private static bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public static void GetStudentScoresFromCourse (string courseName, string userName)
        {
            if (IsQueryForStudentPossible(courseName,userName))
            {
                OutputWriter.DisplayStudent(new KeyValuePair<string, List<int>>(userName, studentsByCourse[courseName][userName]));
            }
        }

        public static void GetAllStudentsFromCourse (string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in studentsByCourse[courseName])
                {
                    OutputWriter.DisplayStudent(studentMarksEntry);
                }
            }
        }
    }
}
