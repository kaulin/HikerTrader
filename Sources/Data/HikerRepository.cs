using HikerTrader.Sources.Models;

namespace HikerTrader.Sources.Data
{
    public interface IHikerRepository
    {
        IEnumerable<Hiker> GetAllHikers();
        Hiker GetHikerById(int hikerId); 
        void InsertHiker(Hiker hiker);
        void UpdateHiker(Hiker hiker);
        void DeleteHiker(int hikerId);
        void Save();
    }

    public class HikerRepository : IHikerRepository
    {
        private readonly List<Hiker> _hikers = new List<Hiker>();

        public IEnumerable<Hiker> GetAllHikers()
        {
            return _hikers;
        }

        public Hiker GetHikerById(int hikerId)
        {
            Hiker? hiker = _hikers.FirstOrDefault(v => v.HikerId == hikerId);
            if (hiker == null)
                throw new ArgumentOutOfRangeException($"No Hiker with HikerId {hikerId}");
            return hiker;
        }

        public void InsertHiker(Hiker hiker)
        {
            _hikers.Add(hiker);
        }

        public void UpdateHiker(Hiker hiker)
        {
            var existingHiker = _hikers.FirstOrDefault(h => h.HikerId == hiker.HikerId);
            if (existingHiker != null)
                _hikers.Remove(existingHiker);
            _hikers.Add(hiker);
        }

        public void DeleteHiker(int hikerId)
        {
            var hiker = _hikers.FirstOrDefault(h => h.HikerId == hikerId);
            if (hiker != null)
            {
                _hikers.Remove(hiker);
            }
        }

        public void Save()
        {
        }
    }
}

