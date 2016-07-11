using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    class Launcher
    {
        static void Main()
        {
            //IOManager.TraverseDirectory(@"D:\Pictures");
            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCourse("Unity");
            Tester.CompareContent(@"C:\Users\Lucho\Desktop\Advanced C#\New folder\test2.txt", @"C:\Users\Lucho\Desktop\Advanced C#\New folder\test3.txt");
        }
    }
}
