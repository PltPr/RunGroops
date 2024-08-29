using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDBContext _context;
        public RaceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRaceByCity(string city)
        {
            return await _context.Races.Where(r=>r.Address.City==city).ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int Id)
        {
            return await _context.Races.Include(a=>a.Address).FirstOrDefaultAsync(r => r.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
