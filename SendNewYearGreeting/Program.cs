using System;
using System.Collections.Generic;
using SendNewYearGreeting.Classes;

namespace SendNewYearGreeting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Send New Year Greeting  새해 인사 보내기 프로그램");
            Console.WriteLine("Powered by Twilio");

            PrintMenu();

            while(true)
            {
                Console.WriteLine();
                Console.Write("> ");
                string command = Console.ReadLine();

                //Execute command
                Command(command);
            }

        }

        static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("> mess          : 전송할 메시지 설정");
            Console.WriteLine("> list          : 메시지를 전송할 전화번호 출력");
            Console.WriteLine("> setlist       : 메시지를 전송할 전화번호 설정");
            Console.WriteLine("> ac            : Twilio 계정 정보 출력");
            Console.WriteLine("> setac         : Twilio 계정 정보 설정");
            Console.WriteLine("> send          : 메시지 전송");
            Console.WriteLine("> help          : 도움말 출력");
            Console.WriteLine("> (명령) --help : 명령별 도움말 출력");
            Console.WriteLine();
        }

        static void Command(string command)
        {
            string cmd = "", option = "";

            //Command Split
            string[] words = command.Split();
            if (0 < words.Length)
                cmd = words[0];
            if (1 < words.Length)
                option = words[1];

            //Execute command
            if (cmd == "mess")
            {
                if (option == "--help")
                {
                    //print mess menual
                    Console.WriteLine("\n[mess (message)]");
                    Console.WriteLine("전송할 메시지 내용을 설정합니다. \"<name>\"을 적으면 이름으로, \"<random>\"을 적으면 새해 인사말로 자동 치환됩니다.");
                }
                else
                    Content.SetBody();
            }
            else if (cmd == "list")
            {
                if (option == "--help")
                {
                    //print list menual
                    Console.WriteLine("\n[list]");
                    Console.WriteLine("메시지를 전송할 전화번호 목록을 출력합니다.");
                }
                else
                {
                    if (!System.IO.File.Exists("address.txt"))
                    {
                        Console.WriteLine("전화번호가 존재하지 않습니다.");
                        return;
                    }

                    List<Address> addresses = AddressData.LoadAddress();

                    //Print whole list
                    Console.WriteLine();
                    for (int i = 0; i < addresses.Count; i++)
                        Console.WriteLine($"{i + 1}: {addresses[i].name}\t\t{addresses[i].tel}");
                }
            }
            else if (cmd == "setlist")
            {
                if (option == "--help")
                {
                    //print setlist menual
                    Console.WriteLine("\n[setlist]");
                    Console.WriteLine("메시지를 전송할 전화번호 목록을 설정합니다. add를 입력해 뒤에 추가하거나, mod를 입력해 특정 번호를 수정하고, del을 입력해 목록을 삭제하고 다시 입력할 수 있습니다.");
                }
                else
                    AddressSet.SetAddress();
            }
            else if (cmd == "ac")
            {
                if (option == "--help")
                {
                    //print ac menual
                    Console.WriteLine("\n[ac (account)]");
                    Console.WriteLine("Twilio 계정 정보를 출력합니다.");
                }
                else
                    Message.PrintAccount();
            }
            else if (cmd == "setac")
            {
                if (option == "--help")
                {
                    //print setac menual
                    Console.WriteLine("\n[setac (set account)]");
                    Console.WriteLine("Twilio 계정 정보를 설정합니다. 메시지 전송을 위해 Account Sid, Auth Token이 필요합니다.");
                }
                else
                    Message.SetAccount();
            }
            else if (cmd == "send")
            {
                if (option == "--help")
                {
                    //print send menual
                    Console.WriteLine("\n[send]");
                    Console.WriteLine("전화번호 목록의 각각에 메시지를 보냅니다. 메시지를 보내기 전, 내용을 확인하고 수정할 수 있습니다.");
                }
                else
                {
                    Message.SendMessageForeachList();
                }
            }
            else if (cmd == "help")
            {
                //print help
                Console.WriteLine("\n[mess (message)]");
                Console.WriteLine("전송할 메시지 내용을 설정합니다. \"<name>\"을 적으면 이름으로, \"<random>\"을 적으면 새해 인사말로 자동 치환됩니다.");

                Console.WriteLine("\n[list]");
                Console.WriteLine("메시지를 전송할 전화번호 목록을 출력합니다.");

                Console.WriteLine("\n[setlist]");
                Console.WriteLine("메시지를 전송할 전화번호 목록을 설정합니다. add를 입력해 뒤에 추가하거나, mod를 입력해 특정 번호를 수정하고, del을 입력해 목록을 삭제하고 다시 입력할 수 있습니다.");

                Console.WriteLine("\n[ac (account)]");
                Console.WriteLine("Twilio 계정 정보를 출력합니다.");

                Console.WriteLine("\n[setac (set account)]");
                Console.WriteLine("Twilio 계정 정보를 설정합니다. 메시지 전송을 위해 Account Sid, Auth Token이 필요합니다.");

                Console.WriteLine("\n[send]");
                Console.WriteLine("전화번호 목록의 각각에 메시지를 보냅니다. 메시지를 보내기 전, 내용을 확인하고 수정할 수 있습니다.");

                Console.WriteLine("\n[clear]");
                Console.WriteLine("콘솔의 텍스트를 모두 지웁니다.");
            }
            else if (cmd == "clear")
            {
                if (option == "--help")
                {
                    //print clear menual
                    Console.WriteLine("\n[clear]");
                    Console.WriteLine("콘솔의 텍스트를 모두 지웁니다.");
                }
                else
                {
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"\"{cmd}\"는 알 수 없는 명령입니다.");
            }
        }





    }
}
