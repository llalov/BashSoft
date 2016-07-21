using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class CommandInterpreter
    {
        public static void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];

            switch (command)
            {
                case "mkdir":
                    if (data.Length == 2)
                    {
                        IOManager.CreateDirectoryInCurrentFolder(data[1]);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidCommandParams);
                    }
                    break;
                case "ls":
                    if (data.Length  == 1)
                    {
                        IOManager.TraverseDirectory();
                    }
                    else
                    {
                        OutputWriter.DisplayException(command + ExceptionMessages.UnNeededParameters);
                    }
                    break;
                case "clear":
                    if (data.Length == 1)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        OutputWriter.DisplayException(command + ExceptionMessages.UnNeededParameters);
                    }
                    break;
                case "cd":
                    if (data.Length == 2)
                    {
                        IOManager.ChangeCurrentDirectoryRelative(data[1]);
                    }
                    else
                    {
                        OutputWriter.DisplayException(command + ExceptionMessages.InvalidCommandParams);
                    }
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidCommand);
                    break;
            }
        }
    }
}
