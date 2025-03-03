using HikerTrader.Sources.Models;

namespace HikerTrader.Sources.Data
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item GetItemByName(string itemName); 
        void InsertItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(string itemName);
        void Save();
    }

    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> _items = new List<Item>
        {
            new Item("Medicine", 5), new Item("Water", 4), new Item("Food", 3)
        };

        public IEnumerable<Item> GetAllItems()
        {
            return _items;
        }

        public Item GetItemByName(string itemName)
        {
            Item? item = _items.FirstOrDefault(v => v.Name == itemName);
            if (item == null)
                throw new ArgumentOutOfRangeException($"No Item with ItemName {itemName}");
            return item;
        }

        public void InsertItem(Item item)
        {
            _items.Add(item);
        }

        public void UpdateItem(Item item)
        {
        }

        public void DeleteItem(string itemName)
        {
        }

        public void Save()
        {
        }
    }
}