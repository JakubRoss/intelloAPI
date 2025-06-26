using Domain.Entities;

namespace Infrastructure.Services
{
    public interface IPozycjaRepository
    {
        public Task<List<PozycjaDokumentu>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<PozycjaDokumentu?> GetByIdAsync(int id);
        Task<PozycjaDokumentu?> UpdateAsync(PozycjaDokumentu pozycjaDokumentu);
        Task<PozycjaDokumentu?> CreateAsync(PozycjaDokumentu pozycjaDokumentu);
    }
}