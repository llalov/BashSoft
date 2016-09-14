using System;
using System.Text;
using System.IO;

namespace BashSoft
{
    public static class PremiumMobileRepository
    {
        public static void SummarizeEmails(string absolutePath)
        {
            string allEmails = string.Empty;
            //int counter = 0;
            Console.OutputEncoding = Encoding.UTF8;
            string row = string.Empty;
            row += "Дата,Име,Фамилия,ЕГН,Телефон,Допълнителен телефон,Email,Избран артикул(и),Допълнителни опции,Финансова институция,Брутна сума,Първоначална вноска,Брой вноски,Месечна вноска,ГПР,ГЛП,\n";
            foreach (string email in Directory.EnumerateFiles(absolutePath, "*.eml"))
            {
                string singleEmail = File.ReadAllText(email);
                string [] mailElements = singleEmail.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < mailElements.Length; i++)
                {
                    if (i > 14 && (i % 2 != 0) && i != 29 & i != mailElements.Length - 1 && i != mailElements.Length -2)
                    {
                        mailElements[i] = mailElements[i].Replace(",", " ");
                        //string[] keyValue = mailElements[i].Split(':');1
                        row += mailElements[i] + ",";
                    }
                }
                allEmails += row + "\n";
                row = string.Empty;
            }

            File.WriteAllText(@"C:\Users\Lucho\Desktop\test.csv", allEmails.ToString(),Encoding.UTF8);

        }
    }
}
