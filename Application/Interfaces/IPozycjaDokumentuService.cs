using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPozycjaDokumentuService
    {
        Task<PozycjaDokumentu?> CreateAsync(CreatePozycjaDokumentuDto createPozycjaDokumentuDto, DokumentPrzyjeciaDto dokumentPrzyjecia);
        Task<bool> DeleteAsync(int id);
        Task<List<PozycjaDokumentuDto>> GetAllAsync();
        Task<PozycjaDokumentuDto> GetByIdAsync(int id);
        Task<PozycjaDokumentuDto?> UpdateAsync(PozycjaDokumentuDto pozycjaDokumentuDto);
    }
}