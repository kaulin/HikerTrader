using System.Globalization;
using HikerTrader.Sources.Data;
using HikerTrader.Sources.Models;

namespace HikerTrader.Sources.Services
{
    public interface IHikerService
    {
        IEnumerable<Hiker> GetAllHikers();
        IEnumerable<Item> GetAllItems();
        Hiker GetHikerById(int hikerId);
        void AddHiker(Hiker hiker);
        void UpdateHiker(Hiker hiker);
        void DeleteHiker(int hikerId);
        public void AddHikerFromInput();
        public void ListHikers();
        public void ExchangeInventories(int hikerId1, int hikerId2);
        public void Run();
    }

    public class HikerService : IHikerService
    {
        private readonly IHikerRepository _hikerRepository;
        private readonly IItemRepository _itemRepository;

        public HikerService(IHikerRepository hikerRepository, IItemRepository itemRepository)
        {
            _hikerRepository = hikerRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<Hiker> GetAllHikers()
        {
            return _hikerRepository.GetAllHikers();
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemRepository.GetAllItems();
        }

        public Hiker GetHikerById(int hikerId)
        {
            return _hikerRepository.GetHikerById(hikerId);
        }

        public void AddHiker(Hiker hiker)
        {
            _hikerRepository.InsertHiker(hiker);
            _hikerRepository.Save();
        }

        public void UpdateHiker(Hiker hiker)
        {
            _hikerRepository.UpdateHiker(hiker);
        }

        public void DeleteHiker(int hikerId)
        {
        }

        public void AddHikerFromInput()
        {
            Console.WriteLine($"Adding hiker {Hiker.GetNextID()}:");
            string name = GetInputString("Provide hiker's name");
            int age = GetInputInt("Provide hiker's age");
            string gender = GetInputString("Provide hiker's gender (M/F/O)");
            double latitude = GetInputDouble("Provide hiker's last known latitude");
            double longitude = GetInputDouble("Provide hiker's last known longitude");
            Hiker hiker = new Hiker(name, age, gender, latitude, longitude);
            foreach (Item item in GetAllItems())
            {
                hiker.AddItemToInventory(item, GetInputInt($"Provide counts of {item.Name} in hiker's inventory"));
            }
            string injured = GetInputString("Is the hiker injured? (yes/no)").ToLower();
            hiker.IsInjured = (injured == "yes" || injured == "y") ? true : false;
            AddHiker(hiker);
            Console.WriteLine($"Hiker {hiker.HikerId} added successfully!\n");
        }
        
        public void ListHikers()
        {
            foreach (Hiker hiker in GetAllHikers())
            {
                hiker.PrintInfo();
            }
        }

        public void ExchangeInventories(int hikerId1, int hikerId2)
        {
            var hiker1 = GetHikerById(hikerId1);
            var hiker2 = GetHikerById(hikerId2);

            if (hiker1 == null ||  hiker2 == null || hiker1.Inventory == null || hiker2.Inventory == null)
                return ;

            int hiker1InventoryValue = hiker1.Inventory.CalculateTotalValue();
            int hiker2InventoryValue = hiker2.Inventory.CalculateTotalValue();

            if (hiker1.IsInjured)
                Console.WriteLine("Trade between Hiker 1 & Hiker 2: Not possible, Hiker 1 is injured");
            else if (hiker2.IsInjured)
                Console.WriteLine("Trade between Hiker 1 & Hiker 2: Not possible, Hiker 2 is injured");
            else if (hiker1InventoryValue < hiker2InventoryValue)
                Console.WriteLine("Trade between Hiker 1 & Hiker 2: Not possible, Hiker 1 has less inventory points to exchange");
            else if (hiker1InventoryValue > hiker2InventoryValue)
                Console.WriteLine("Trade between Hiker 1 & Hiker 2: Not possible, Hiker 1 has more inventory points to exchange");
            else
            {
                var tempInventory = new Inventory(0);
                tempInventory.ReplaceInventory(hiker1.Inventory);
                hiker1.Inventory.ReplaceInventory(hiker2.Inventory);
                hiker2.Inventory.ReplaceInventory(tempInventory);

                UpdateHiker(hiker1);
                UpdateHiker(hiker2);
                
                Console.WriteLine("Trade between Hiker 1 & Hiker 2: Trade completed, see updated info below:");
                ListHikers();
            }
        }

        public void Run()
        {
            try
            {            
                AddHikerFromInput();
                AddHikerFromInput();
                ListHikers();
                ExchangeInventories(1, 2);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error, caught exception:\n{e}\nTerminating application!");
            }
        }

        private static double GetInputDouble(string promptMessage)
        {
            bool success = false;
            double result = 0;
            while (!success)
            {
                string line = GetInputString(promptMessage);
                try
                {
                    result = double.Parse(line, NumberFormatInfo.InvariantInfo);
                    success = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please provide a floating point value!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please provide a value in range: double!");
                }
            }
            return result;
        }

        private static int GetInputInt(string promptMessage)
        {
            bool success = false;
            int result = 0;
            while (!success)
            {
                string line = GetInputString(promptMessage);
                try
                {
                    result = int.Parse(line);
                    if (result >= 0) 
                        success = true;
                    else
                        Console.WriteLine("Please provide a positive integer value!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please provide a positive integer value!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please provide a value in range: positive integer!");
                }
            }
            return result;
        }

        private static string GetInputString(string promptMessage)
        {
            string? line = null;
            while (string.IsNullOrEmpty(line))
            {
                Console.Write(promptMessage + ": ");
                line = Console.ReadLine();
                if (line != null)
                    line = line.Trim();
            }
            return line;
        }
    }
}