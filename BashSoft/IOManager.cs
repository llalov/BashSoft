using System.Collections.Generic;
using System.IO;
using System;
using System.Net;

namespace BashSoft
{
    public static class IOManager
    {
        public static void TraverseDirectory()
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            OutputWriter.WriteMessageOnNewLine("*********************************************************************************");
            while(subFolders.Count != 0)
            {
                string currentPath = subFolders.Dequeue();
                int identation = currentPath.Split('\\').Length - initialIdentation;

                //OutputWriter.WriteMessageOnNewLine(currentPath);
                foreach(string directoryPath in Directory.GetDirectories(currentPath))
                {
                    subFolders.Enqueue(directoryPath);
                }
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));
            }
            OutputWriter.WriteMessageOnNewLine("*********************************************************************************");
        }

        public static void ShowDirectory()
        {
            try
            {
                foreach (var file in Directory.GetFiles(SessionData.currentPath))
                {
                    int indexOfLastSlash = file.LastIndexOf("\\");
                    string fileName = file.Substring(indexOfLastSlash);
                    OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + fileName);
                }

                foreach (var directory in Directory.GetDirectories(SessionData.currentPath))
                {
                    int indexOfLastSlash = directory.LastIndexOf("\\");
                    string directoryName = directory.Substring(indexOfLastSlash);
                    OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + directoryName);
                }
            }
            catch (System.UnauthorizedAccessException)
            {

                OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessExceptionMessage);
            }
            
        }

        public static void CreateDirectoryInCurrentFolder(string folderName)
        {
            try
            {
                string path = SessionData.currentPath + "\\" + folderName;
                Directory.CreateDirectory(path);
                SessionData.currentPath = path;
            }
            catch (ArgumentException)
            {
                OutputWriter.DisplayException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public static void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToGoHigherInPartitionHierarchy);
                }
                
            }
            else if (!Directory.Exists(relativePath))
            {
                string currentPath = SessionData.currentPath;
                relativePath = relativePath.TrimStart('\\');
                currentPath += '\\' + relativePath;
                currentPath = currentPath.TrimEnd('/', '\\');
                if (Directory.Exists(currentPath))
                {
                    ChangeCurrentDirectoryAbsolute(currentPath);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                }
            }
            else if (Directory.Exists(relativePath))
            {
                ChangeCurrentDirectoryAbsolute(relativePath);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        public static void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }

            SessionData.currentPath = absolutePath;
        }

        public static void DownloadFile(string url)
        {
            url = url.Trim(' ');
            try
            {
                int indexOfLastSlash = url.LastIndexOf('/');
                string fileName = url.Substring(indexOfLastSlash + 1);
                var currentDir = SessionData.currentPath;

                WebRequest request = WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response != null || response.StatusCode == HttpStatusCode.OK)
                {
                    using (WebClient client = new WebClient())
                    {
                        var uri = new Uri(url);
                        client.DownloadFileAsync(uri, currentDir + "\\" + fileName);
                        OutputWriter.WriteMessageOnNewLine("Download complete.");
                    }
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InvalidURL);
                }
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        public static void OpenFile(string userInput)
        {
            var trimedUserInput = userInput.Trim();
            try
            {
                if (File.Exists(trimedUserInput) || Directory.Exists(trimedUserInput))
                {
                    System.Diagnostics.Process.Start(trimedUserInput);
                }

                else if (!Directory.Exists(trimedUserInput) || !File.Exists(trimedUserInput)) 
                {
                    var absolutePath = SessionData.currentPath;
                    absolutePath += '\\' + trimedUserInput;

                    if (File.Exists(absolutePath) || Directory.Exists(absolutePath))
                    {
                        System.Diagnostics.Process.Start(absolutePath);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                        OutputWriter.WriteMessageOnNewLine(absolutePath);
                    }
                }
                
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        public static void Help()
        {
            OutputWriter.WriteMessageOnNewLine("************************************************************************************************************************");
            OutputWriter.WriteMessageOnNewLine("cd..                            'Returns one directory back.'");
            OutputWriter.WriteMessageOnNewLine("cd <absolute path>              'Changes current directory by an absolute path.'");
            OutputWriter.WriteMessageOnNewLine("cd <folder name>                'Changes current directory to a folder that existst in it.'");
            OutputWriter.WriteMessageOnNewLine("mkdir <folder name>             'Creates a folder in the current directory.'");
            OutputWriter.WriteMessageOnNewLine("ls                              'Lists all files and folders in the current directory.'");
            OutputWriter.WriteMessageOnNewLine("open <file name/folder name>    'Opens a file or directory. Can be used also with absolute paths.'");
            OutputWriter.WriteMessageOnNewLine("download <url>                  'Downloads a file to the current directory.'");
            OutputWriter.WriteMessageOnNewLine("readDb                          'Reads a txt file in the current directory in the folowing format: http:\\github.com\'");
            OutputWriter.WriteMessageOnNewLine("filter <course name>            'Returns all the students from the folowing course. (Example file: )'");
            OutputWriter.WriteMessageOnNewLine("clear                           'Clear the console window.'");
            OutputWriter.WriteMessageOnNewLine("quit                            'Exit the console window.'");
            OutputWriter.WriteMessageOnNewLine("help                            'Lists all the commands with their descriptions.'");
            OutputWriter.WriteMessageOnNewLine("************************************************************************************************************************");
        }
    }
}
