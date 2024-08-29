using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club>GetByIdAsync(int Id);
        Task<IEnumerable<Club>>GetClubByCity(string city);
        bool Add(Club club);
        bool Delete(Club club);
        bool Save();
        bool Update(Club club);
    }
}
