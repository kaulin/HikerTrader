namespace HikerTrader.Sources.Models
{
    public class Item
    {
        public string Name { get; private set; }
        public int PointValue { get; private set; }

        public Item(string name, int pointValue)
        {
            Name = name;
            PointValue = pointValue;
        }
    }
}