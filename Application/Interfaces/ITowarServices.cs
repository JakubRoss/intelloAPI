using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITowarServices
    {
        Task<TowarDto?> CreateAsync(CreateTowarDto Towar);
        Task<bool> DeleteAsync(int id);
        Task<List<TowarDto>> GetAllAsync();
        Task<Towar?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(TowarDto Towar);
    }
}