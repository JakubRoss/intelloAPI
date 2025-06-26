using Domain.Entities;

namespace Infrastructure.Services
{
    public interface IKontrahentRepository
    {
        /// <param name="dto"></param>
        /// <returns>Returns ID of new kontrahent</returns>
        Task<int> CreateAsync(Kontrahent kontrahent);
        /// <param name="id"></param>
        /// <returns>Return true if deleted or return false if not (kontrahent doesn't exist or have dokumentPrzyjecia assigned to himself</returns>
        Task<bool> DeleteAsync(int id);
        /// <returns>Returns list of kontrahents or empty list</returns>
        Task<IEnumerable<Kontrahent>> GetAllAsync();
        /// <param name="id"></param>
        /// <returns>Return kontrahent of choosen id or null if doesn't exist</returns>
        Task<Kontrahent?> GetByIdAsync(int id);
        Task<int> UpdateAsync(Kontrahent kontrahent);
    }
}