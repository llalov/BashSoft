using System.Collections.Generic;
using System.IO;

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
        }

        public static void ShowDirectory()
        {
            foreach (var file in Directory.GetFiles(SessionData.currentPath))
            {
                int indexOfLastSlash = file.LastIndexOf("\\");
                string fileName = file.Substring(indexOfLastSlash);
                OutputWriter.WriteMessageOnNewLine(new string('-',indexOfLastSlash) + fileName);
            }

            foreach (var directory in Directory.GetDirectories(SessionData.currentPath))
            {
                int indexOfLastSlash = directory.LastIndexOf("\\");
                string directoryName = directory.Substring(indexOfLastSlash);
                OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + directoryName);
            }
        }

        public static void CreateDirectoryInCurrentFolder(string folderName)
        {
            string path = SessionData.currentPath + "\\" + folderName;
            Directory.CreateDirectory(path); 
        }

        public static void ChangeCurrentDirectoryRelative(string relativaPath)
        {

        }

        public static void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {

        }
    }
}
