using System;
using System.Text;
using System.IO;

namespace BashSoft
{
    public static class PremiumMobileRepository
    {
        public static void SummarizeEmails(string absolutePath)
        {
            var csv = new StringBuilder();
            string allEmails = string.Empty;
            int counter = 0;
            //Console.OutputEncoding = Encoding.UTF8;
            foreach (string email in Directory.EnumerateFiles(absolutePath, "*.eml"))
            {
                string singleEmail = File.ReadAllText(email);
                string[] mailElements = singleEmail.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string row = string.Empty;
                for (int i = 0; i < mailElements.Length; i++)
                {
                    if (i > 14 && i != 18 && i != 20 && i != 22)
                    {
                        row += mailElements[i] + ",";
                    }
                } 
                allEmails += row + "\n";
            }

            File.WriteAllText(@"C:\Users\Lucho\Desktop\test.csv", allEmails.ToString(),Encoding.UTF8);

        }
    }
}
