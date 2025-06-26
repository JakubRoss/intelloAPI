using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repository;

namespace Application.Services
{
    internal class TowarServices : ITowarServices
    {
        private readonly ITowarRepository towarRepository;

        public TowarServices(ITowarRepository towarRepository)
        {
            this.towarRepository = towarRepository;
        }
        public async Task<List<TowarDto>> GetAllAsync()
        {
            var towary = await towarRepository.GetAllAsync();

            return towary.Select(t => new TowarDto
            {
                Id = t.Id,
                NazwaTowaru = t.NazwaTowaru,
                JednostkaMiary = t.JednostkaMiary
            }).ToList();
        }
        public async Task<Towar?> GetByIdAsync(int id)
        {
            var p = await towarRepository.GetByIdAsync(id);

            return p;
        }

        public async Task<bool> UpdateAsync(TowarDto Towar)
        {
            if (Towar == null || string.IsNullOrEmpty(Towar.NazwaTowaru) || string.IsNullOrEmpty(Towar.JednostkaMiary)) return false;
            var p = await towarRepository.UpdateAsync(new Towar
            {
                Id=Towar.Id,
                NazwaTowaru = Towar.NazwaTowaru,
                JednostkaMiary = Towar.JednostkaMiary
            });

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await towarRepository.DeleteAsync(id);
        }

        public async Task<TowarDto?> CreateAsync(CreateTowarDto Towar)
        {
            if (Towar == null || string.IsNullOrEmpty(Towar.Nazwa) || string.IsNullOrEmpty(Towar.JednostkaMiary))
                return null;

            var createdTowar = await towarRepository.CreateAsync(new Towar
            {
                NazwaTowaru = Towar.Nazwa,
                JednostkaMiary = Towar.JednostkaMiary,
            });
            if (createdTowar == null)
                return null;
            return new TowarDto
            {
                Id = createdTowar.Id,
                NazwaTowaru = createdTowar.NazwaTowaru,
                JednostkaMiary = createdTowar.NazwaTowaru
            };
        }
    }
}
