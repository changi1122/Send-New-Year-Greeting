using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SendNewYearGreeting.Classes
{
    /// <summary>
    /// Class about message content
    /// </summary>
    public static class Content
    {
        public static string Body
        {
            get
            {
                if (!File.Exists("content.txt"))
                    return "";

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

        public static void SetBody()
        {
            //Get message content
            Console.WriteLine("전송할 메시지를 입력해주세요.");
            Console.WriteLine("\"<name>\": 수신자의 이름으로 치환");
            Console.WriteLine("\"<random>\": 다양한 새해 인사말로 치환");
            Console.WriteLine();
            Console.WriteLine($"현재 메시지 : {Body}");
            Console.Write("전송할 메시지: ");
            Body = Console.ReadLine();

            return;
        }
    }
}
