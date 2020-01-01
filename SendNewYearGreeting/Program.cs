using System;
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

            Console.WriteLine("Hello World!");
        }

        static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("> mess          : 전송할 메시지 설정");
            Console.WriteLine("> list          : 메시지를 전송할 전화번호 출력");
            Console.WriteLine("> set list      : 메시지를 전송할 전화번호 설정");
            Console.WriteLine("> ac            : Twilio 계정 정보 출력");
            Console.WriteLine("> set ac        : Twilio 계정 정보 설정");
            Console.WriteLine("> send          : 메시지 전송");
            Console.WriteLine("> help          : 도움말 출력");
            Console.WriteLine("> (명령) --help : 명령별 도움말 출력");
            Console.WriteLine();
        }
    }
}
