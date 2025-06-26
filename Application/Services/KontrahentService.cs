using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Services;

namespace Application.Services
{
    internal class KontrahentService : IKontrahentService
    {
        private readonly IDokumentRepository dokumentRepository;
        private readonly IPozycjaRepository pozycjaRepository;
        private readonly IKontrahentRepository kontrahentRepository;

        public KontrahentService(IDokumentRepository dokumentRepository, IPozycjaRepository pozycjaRepository, IKontrahentRepository kontrahentRepository)
        {
            this.dokumentRepository = dokumentRepository;
            this.pozycjaRepository = pozycjaRepository;
            this.kontrahentRepository = kontrahentRepository;
        }

        public async Task<int> Create(CreateKontrahentDto kontrahentDto)
        {
            if (kontrahentDto == null)
                throw new Exception("Kontrahent is required");
            var kontrahent = new Kontrahent
            {
                Nazwa = kontrahentDto.Nazwa,
                Symbol = kontrahentDto.Symbol,

            };
            return await kontrahentRepository.CreateAsync(kontrahent);
        }
        public async Task<List<KontrahentDto>> GetAllAsync()
        {
            var kontrahenci = (await kontrahentRepository.GetAllAsync()).ToList();
            if (kontrahenci == null)
                return new List<KontrahentDto>();
            return kontrahenci.Select(p => new KontrahentDto
            {
                Id = p.Id,
                Nazwa = p.Nazwa,
                Symbol = p.Symbol,
            }).ToList();
        }

        public async Task<KontrahentDto?> GetByIdAsync(int id)
        {
            var kontrahent = await kontrahentRepository.GetByIdAsync(id);
            if (kontrahent == null)
                return null;

            return new KontrahentDto { 
                Id = kontrahent.Id,
                Symbol = kontrahent.Symbol,
                Nazwa = kontrahent.Nazwa
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await kontrahentRepository.DeleteAsync(id);
        }
        public async Task<int> UpdateAsync(CreateKontrahentDto kontrahentDto,int kontrahentID)
        {
            if (kontrahentDto == null)
                throw new Exception("Kontrahent is required");
            var kontrahentToUpdate = await kontrahentRepository.GetByIdAsync(kontrahentID);
            if (kontrahentToUpdate == null)
                return 0;

            if (kontrahentToUpdate == null)
                throw new Exception("Kontrahent is required");

            kontrahentToUpdate.Symbol = string.IsNullOrEmpty(kontrahentDto.Symbol) ? kontrahentToUpdate.Symbol : kontrahentDto.Symbol;
            kontrahentToUpdate.Nazwa = string.IsNullOrEmpty(kontrahentDto.Nazwa) ? kontrahentToUpdate.Nazwa : kontrahentDto.Nazwa;

            return await kontrahentRepository.UpdateAsync(kontrahentToUpdate);
        }
    }
}
