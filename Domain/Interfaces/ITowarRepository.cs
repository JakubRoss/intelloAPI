using Domain.Entities;

namespace Infrastructure.Repository
{
    public interface ITowarRepository
    {
        Task<Towar?> CreateAsync(Towar towar);
        Task<bool> DeleteAsync(int id);
        Task<List<Towar>> GetAllAsync();
        Task<Towar?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Towar Towar);
    }
}