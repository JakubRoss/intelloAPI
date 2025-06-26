using Domain.Entities;

namespace Infrastructure.Services
{
    public interface IDokumentRepository
    {
        Task<int> CreateAsync(DokumentPrzyjecia dokumentPrzyjecia);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DokumentPrzyjecia>> GetAllAsync();
        Task<DokumentPrzyjecia?> GetByIdAsync(int id);
        Task<DokumentPrzyjecia> UpdateAsync(DokumentPrzyjecia dokumentPrzyjecia);
    }
}