using System.Text;

namespace HikerTrader.Sources.Models
{
    public class Hiker
    {
        private static int nextHikerId = 1;
        public int HikerId { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Gender { get; private set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsInjured { get; set; }
        public Inventory Inventory { get; private set; }

        public static int GetNextID()
        {
            return nextHikerId;
        }

        public Hiker(string name, int age, string gender, double longitude, double latitude)
        {
            HikerId = nextHikerId++;
            Name = name;
            Age = age;
            Gender = gender;
            Longitude = longitude;
            Latitude = latitude;
            IsInjured = false;
            Inventory = new Inventory(HikerId);
        }

        public void AddItemToInventory(Item item, int quantity)
        {
            Inventory.AddItem(item, quantity);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{HikerId}.  Hiker {HikerId}");
            Console.WriteLine($"    a.  Name: {Name}");
            Console.WriteLine($"    b.  Age: {Age}");
            Console.WriteLine($"    c.  Gender: {Gender}");
            Console.WriteLine($"    d.  Last Location: {Longitude}, {Latitude}");
            Console.WriteLine($"    e.  Inventory Items");
            int i = 1;
            foreach (var (item, quantity) in Inventory.GetItems())
            {
                Console.WriteLine($"            {ToRomanNumeral(i++)}.  {item.Name}: {quantity}");
            }
            Console.WriteLine($"     f.  Injured: " + (IsInjured ? "Yes" : "No"));
            Console.WriteLine("");
        }

        // Converter from https://stackoverflow.com/a/23303475
        private static string ToRomanNumeral(int value)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            StringBuilder sb = new StringBuilder();
            int remain = value;
            while (remain > 0)
            {
                if (remain >= 1000) { sb.Append("m"); remain -= 1000; }
                else if (remain >= 900) { sb.Append("cm"); remain -= 900; }
                else if (remain >= 500) { sb.Append("d"); remain -= 500; }
                else if (remain >= 400) { sb.Append("cd"); remain -= 400; }
                else if (remain >= 100) { sb.Append("c"); remain -= 100; }
                else if (remain >= 90) { sb.Append("xc"); remain -= 90; }
                else if (remain >= 50) { sb.Append("l"); remain -= 50; }
                else if (remain >= 40) { sb.Append("xl"); remain -= 40; }
                else if (remain >= 10) { sb.Append("x"); remain -= 10; }
                else if (remain >= 9) { sb.Append("ix"); remain -= 9; }
                else if (remain >= 5) { sb.Append("v"); remain -= 5; }
                else if (remain >= 4) { sb.Append("iv"); remain -= 4; }
                else if (remain >= 1) { sb.Append("i"); remain -= 1; }
                else throw new Exception("Unexpected error.");
            }
            return sb.ToString();
        }
    }
}