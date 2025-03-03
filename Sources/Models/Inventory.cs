namespace HikerTrader.Sources.Models
{
    public class Inventory
    {
        public int InventoryId { get; private set; }
        private Dictionary<Item, int> _items = new();

        public Inventory(int id)
        {
            InventoryId = id;
        }

        public void AddItem(Item item, int quantity)
        {
            if (_items.ContainsKey(item))
                _items[item] += quantity;
            else
                _items[item] = quantity;
        }

        public int CalculateTotalValue()
        {
            int sum = 0;
            foreach (var (item, quantity) in _items)
            {
                sum += item.PointValue * quantity;
            }
            return sum;
        }

        public void ReplaceInventory(Inventory newInventory)
        {
            _items = newInventory._items;
        }

        public Dictionary<Item, int> GetItems() => new(_items);
    }
}
