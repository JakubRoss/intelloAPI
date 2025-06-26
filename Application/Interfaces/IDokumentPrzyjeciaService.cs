using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDokumentPrzyjeciaService
    {
        Task<int> Create(CreateDokumentPrzyjeciaDto dokumentPrzyjecia);
        Task<List<DokumentPrzyjeciaDto>> GetAllAsync();
        Task<DokumentPrzyjeciaDto> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<DokumentPrzyjeciaDto?> UpdateAsync(UpdateDokumentPrzyjeciaDto dokumentPrzyjeciaDto, int dokumentPrzyjeciaID);
    }
}