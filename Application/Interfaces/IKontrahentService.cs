using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IKontrahentService
    {
        Task<int> Create(CreateKontrahentDto kontrahentDto);
        Task<List<KontrahentDto>> GetAllAsync();
        Task<KontrahentDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<int> UpdateAsync(CreateKontrahentDto kontrahentDto, int kontrahentID);
    }
}