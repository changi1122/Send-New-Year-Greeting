using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SendNewYearGreeting.Classes
{
    public static class Content
    {
        public static string Body
        {
            get
            {
                if (!File.Exists("content.txt"))
                {
                    Console.WriteLine("전송할 메시지가 존재하지 않습니다.");
                    return "";
                }

                //Read message body
                FileStream fs = new FileStream("content.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string body = sr.ReadToEnd();
                sr.Close();
                return body;
            }

            set
            {
                //Delete temporary file
                if (File.Exists("_content.txt"))
                    File.Delete("_content.txt");

                //Write new temporary file
                FileStream fs = new FileStream("_content.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(value);
                sw.Close();

                //Swap new file for account file
                if (File.Exists("content.txt"))
                    File.Delete("content.txt");
                File.Move("_content.txt", "content.txt");
            }
        }

    }
}
