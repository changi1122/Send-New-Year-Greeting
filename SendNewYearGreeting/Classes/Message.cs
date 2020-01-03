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
        private static string phoneNumber
        {
            get
            {
                //Read phone number
                string _phoneNumber;
                FileStream fs = new FileStream("account.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                sr.ReadLine();
                sr.ReadLine();
                _phoneNumber = sr.ReadLine();
                sr.Close();
                fs.Close();

                return _phoneNumber;
            }
        }

        public static void SetAccount()
        {
            string accountSid;
            string authToken;
            string phoneNumber;

            //Delete temporary file
            if (File.Exists("_account.txt"))
                File.Delete("_account.txt");

            //Get Twilio accountSid and authToken, phone number
            Console.WriteLine("Twilio 계정 정보를 입력해주세요.");
            Console.Write("Twilio Account Sid: ");
            accountSid = Console.ReadLine();
            Console.Write("Twilio Auth Token : ");
            authToken = Console.ReadLine();
            Console.Write("발신 번호         : ");
            phoneNumber = Console.ReadLine();

            //Write new temporary file
            FileStream fs = new FileStream("_account.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(accountSid);
            sw.WriteLine(authToken);
            sw.WriteLine(phoneNumber);
            sw.Close();
            fs.Close();

            //Swap new file for account file
            if (File.Exists("account.txt"))
                File.Delete("account.txt");
            File.Move("_account.txt", "account.txt");

            return;
        }

        public static void PrintAccount()
        {
            if (!File.Exists("account.txt"))
            {
                Console.WriteLine("Twilio 계정 정보가 존재하지 않습니다.");
                return;
            }

            string accountSid;
            string authToken;
            string phoneNumber;

            //Read Twilio accountSid and authToken
            FileStream fs = new FileStream("account.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            accountSid = sr.ReadLine();
            authToken = sr.ReadLine();
            phoneNumber = sr.ReadLine();
            sr.Close();
            fs.Close();

            Console.WriteLine($"Twilio Account Sid: {accountSid}");
            Console.WriteLine($"Twilio Auth Token : {authToken}");
            Console.WriteLine($"발신 번호         : {phoneNumber}");

            return;
        }

        //Check if the account file has accountSid and authToken exists
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
            FileStream fs = new FileStream("account.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            accountSid = sr.ReadLine();
            authToken = sr.ReadLine();
            sr.Close();
            fs.Close();

            try
            {
                //Send Message by Twilio
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(from),
                    to: new Twilio.Types.PhoneNumber(to)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catch: " + ex.Message);
            }

            return;
        }

        public static void SendMessageForeachList()
        {
            List<Address> addresses = AddressData.LoadAddress();
            
            for (int i = 0; i < addresses.Count; i++)
            {
                string from = phoneNumber;
                string to = addresses[i].tel;

                //Substitution
                string body = Content.GetSubstitutedBody(addresses[i].name);

                while(true)
                {
                    Console.WriteLine();

                    Console.WriteLine($"발신  : {from}");
                    Console.WriteLine($"수신  : {to}  {addresses[i].name}");
                    Console.WriteLine($"메시지: {body}");
                    Console.WriteLine();

                    Console.WriteLine("전송하겠습니까? (y: 전송, n: 취소, m: 메시지 수정)");
                    Console.Write("> ");
                    string cmd = Console.ReadLine();

                    if (cmd == "y" || cmd == "yes" || cmd == "예")
                    {
                        SendMessage(body, from, to);
                        break;
                    }
                    else if (cmd == "n" || cmd == "no" || cmd == "아니오")
                        break;
                    else if (cmd == "m" || cmd == "modify" || cmd == "수정")
                    {
                        Console.WriteLine($"현재 메시지 : {body}");
                        Console.Write("새로운 메시지: ");
                        body = Console.ReadLine();
                    }
                }
            }






        }

    }
}
