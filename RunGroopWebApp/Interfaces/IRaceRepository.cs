using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int Id);
        Task<Race> GetByIdAsyncNoTracking(int Id);
        Task<IEnumerable<Race>> GetAllRaceByCity(string city);
        bool Add(Race race);
        bool Delete(Race race);
        bool Save();
        bool Update(Race race);
    }
}
