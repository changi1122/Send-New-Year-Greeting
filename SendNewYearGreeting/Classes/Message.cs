using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SendNewYearGreeting.Classes
{
    /// <summary>
    /// Class about sending messages
    /// </summary>
    public static class Message
    {
        public static void SetAccount()
        {
            string accountSid;
            string authToken;

            //Delete temporary file
            if (File.Exists("_account.txt"))
                File.Delete("_account.txt");

            //Get Twilio accountSid and authToken
            Console.WriteLine("Twilio 계정 정보를 입력해주세요.");
            Console.Write("Twilio Account Sid: ");
            accountSid = Console.ReadLine();
            Console.Write("Twilio Auth Token : ");
            authToken = Console.ReadLine();

            //Write new temporary file
            FileStream fs = new FileStream("_account.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(accountSid);
            sw.WriteLine(authToken);
            sw.Close();

            //Swap new file for account file
            if (File.Exists("account.txt"))
                File.Delete("account.txt");
            File.Move("_account.txt", "account.txt");

            return;
        }

        // Check if the account file has accountSid and authToken exists
        public static bool CheckAccountExists()
        {
            string target = "account.txt";

            return File.Exists(target);
        }

        public static void SendMessage(string body, string from, string to)
        {
            if (!File.Exists("account.txt"))
            {
                Console.WriteLine("Twilio 계정 정보가 존재하지 않습니다.");
                return;
            }

            string accountSid;
            string authToken;

            //Read Twilio accountSid and authToken
            FileStream fs = new FileStream("account.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            accountSid = sr.ReadLine();
            authToken = sr.ReadLine();

            //Send Message by Twilio
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(from),
                to: new Twilio.Types.PhoneNumber(to)
            );

            return;
        }

    }
}
