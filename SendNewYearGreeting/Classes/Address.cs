using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SendNewYearGreeting.Classes
{
    /// <summary>
    /// Class about setting address
    /// </summary>
    public static class AddressSet
    {
        public static void SetAddress()
        {
            List<Address> addresses = AddressData.LoadAddress();

            //Print option
            Console.WriteLine();
            Console.WriteLine("> add : 추가");
            Console.WriteLine("> mod : 수정");
            Console.WriteLine("> del : 삭제 후 입력");
            Console.WriteLine();

            Console.WriteLine(">");
            string cmd = Console.ReadLine();
            
            if (cmd == "add")
            {
                AddtoAddress(addresses);
            }
            else if (cmd == "mod")
            {
                ModifyAddress(addresses);
            }
            else if (cmd == "del")
            {
                addresses.Clear();
                AddtoAddress(addresses);
            }

            AddressData.SaveAddress(addresses);
            return;
        }

        private static void AddtoAddress(List<Address> addresses)
        {
            Console.WriteLine();
            Console.WriteLine("전송할 전화번호를 추가합니다. (미입력 후 엔터를 눌러 종료)");

            while(true)
            {
                Console.Write("이름    : ");
                string name = Console.ReadLine();

                if (name == "")
                    break;

                Console.Write("전화번호: ");
                string tel = Console.ReadLine();

                if (tel == "")
                    break;

                Address add = new Address(name, tel);
                addresses.Add(add);
            }
            return;
        }

        private static void ModifyAddress(List<Address> addresses)
        {
            while (true)
            {
                //Print whole list
                Console.WriteLine();
                for (int i = 0; i < addresses.Count; i++)
                    Console.WriteLine($"{i + 1}: {addresses[i].name}\t\t{addresses[i].tel}");

                Console.WriteLine();
                Console.WriteLine("수정할 전화번호의 순번을 입력하세요. (미입력 후 엔터를 눌러 종료)");

                Console.Write(">");
                string strNumber = Console.ReadLine();
                if (strNumber == "")
                    break;

                //string to int, check extent
                int number = int.Parse(strNumber) - 1;
                if (number < 0 || addresses.Count <= number)
                {
                    Console.WriteLine("순번 범위를 초과하였습니다.");
                    break;
                }

                //Print old one, get data
                Console.WriteLine();
                Console.WriteLine($"{number + 1}: {addresses[number].name}\t\t{addresses[number].tel}");
                Console.WriteLine();

                Console.Write("새 이름    : ");
                addresses[number].name = Console.ReadLine();

                Console.Write("새 전화번호: ");
                addresses[number].tel = Console.ReadLine();
            }
        }
    }

    /// <summary>
    /// Class about saving and loading address
    /// </summary>
    static class AddressData
    {
        public static void SaveAddress(List<Address> addresses)
        {
            //Delete temporary file
            if (File.Exists("_address.txt"))
                File.Delete("_address.txt");

            //Write new temporary file
            FileStream fs = new FileStream("_address.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < addresses.Count; i++)
            {
                sw.WriteLine(addresses[i].name);
                sw.WriteLine(addresses[i].tel);
            }
            sw.Close();

            //Swap new file for account file
            if (File.Exists("address.txt"))
                File.Delete("address.txt");
            File.Move("_address.txt", "address.txt");

            return;
        }

        public static List<Address> LoadAddress()
        {
            if (!File.Exists("address.txt"))
            {
                Console.WriteLine("전화번호가 존재하지 않습니다.");
                return null;
            }

            //Address List
            List<Address> addresses = new List<Address>();

            //Read address file, Read to end and split
            FileStream fs = new FileStream("address.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader("address.txt", Encoding.UTF8);
            string temp = sr.ReadToEnd();
            sr.Close();
            string[] lines = temp.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            //Add to list
            for (int i = 0; i < lines.Length; i += 2)
            {
                Address add = new Address(lines[i], lines[i + 1]);
                addresses.Add(add);
            }

            return addresses;
        }
    }

    /// <summary>
    /// Address data type
    /// </summary>
    class Address
    {
        public string name;
        public string tel;

        public Address(string _name, string _tel)
        {
            this.name = _name;
            this.tel = _tel;
        }
    }
}
