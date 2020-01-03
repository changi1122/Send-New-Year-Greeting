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
                fs.Close();
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
                fs.Close();

                //Swap new file for account file
                if (File.Exists("content.txt"))
                    File.Delete("content.txt");
                File.Move("_content.txt", "content.txt");
            }
        }

        public static string GetSubstitutedBody(string name)
        {
            string body = Body;
            
            //Substitution
            if (body.Contains("<name>"))
            {
                body = body.Replace("<name>", name);
            }
            if (body.Contains("<random>"))
            {
                body = body.Replace("<random>", GetGreetingStrings());
            }

            return body;
        }

        private static string GetGreetingStrings()
        {
            string[] GREETINGSTRINGS = {
                "새해에는 행운과 평안이 가듯하기를 기원합니다",
                "새해에는 소망하는 일 모두 이루세요!",
                "가족 모두 행복한 한 해 보내세요!",
                "새해 복 많이 받으시고 올 한 해도 건강해지길 바랍니다.",
                "새해에도 웃음 가득한 한 해 되세요.",
                "새해에도 늘 행복하고 건강하세요",
                "새해에도 좋은 일만 가득하기를 기원합니다.",
                "반짝반짝 빛나는 한 해가 되기를 기원합니다."
            };

            Random r = new Random();
            string gs = GREETINGSTRINGS[r.Next(0, GREETINGSTRINGS.Length)];

            return gs;
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
