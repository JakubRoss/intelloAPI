using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Services;


namespace Application.Services
{
    internal class PozycjaDokumentuService : IPozycjaDokumentuService
    {
        private readonly IPozycjaRepository pozycjaRepository;

        public PozycjaDokumentuService(IPozycjaRepository pozycjaRepository)
        {
            this.pozycjaRepository = pozycjaRepository;
        }

        public async Task<PozycjaDokumentu?> CreateAsync(CreatePozycjaDokumentuDto createPozycjaDokumentuDto, DokumentPrzyjeciaDto dokumentPrzyjecia)
        {
            var pozycjaDokumentu = await pozycjaRepository.GetByIdAsync(dokumentPrzyjecia.Id);
            return await pozycjaRepository.CreateAsync(new PozycjaDokumentu
            {
                DokumentPrzyjeciaId = dokumentPrzyjecia.Id,
                Ilosc = createPozycjaDokumentuDto.Ilosc,
                TowarId = createPozycjaDokumentuDto.TowarId,
            });
        }
        public async Task<List<PozycjaDokumentuDto>> GetAllAsync()
        {
            var pozcyjeDokumentu = await pozycjaRepository.GetAllAsync();
            if (pozcyjeDokumentu == null)
                return new List<PozycjaDokumentuDto>();
            return pozcyjeDokumentu.Select(p => new PozycjaDokumentuDto
            {
                Id = p.Id,
                NazwaTowaru = p.Towar.NazwaTowaru,
                JednostkaMiary = p.Towar.JednostkaMiary,
                Ilosc = p.Ilosc
            }).ToList();
        }

        public async Task<PozycjaDokumentuDto> GetByIdAsync(int id)
        {
            var pozycjaDokumentu = await pozycjaRepository.GetByIdAsync(id);
            if (pozycjaDokumentu == null)
                throw new Exception("pozycjaDokumentu doesnt exist");

            return new PozycjaDokumentuDto
            {
                Id = pozycjaDokumentu.Id,
                TowarId = pozycjaDokumentu.TowarId,
                NazwaTowaru = pozycjaDokumentu.Towar.NazwaTowaru,
                Ilosc = pozycjaDokumentu.Ilosc,
                JednostkaMiary = pozycjaDokumentu.Towar.JednostkaMiary
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await pozycjaRepository.DeleteAsync(id);
        }
        public async Task<PozycjaDokumentuDto?> UpdateAsync(PozycjaDokumentuDto pozycjaDokumentuDto)
        {
            var istniejącaPozycja = await pozycjaRepository.GetByIdAsync(pozycjaDokumentuDto.Id);
            if (istniejącaPozycja == null)
                return null;
            istniejącaPozycja.Ilosc = pozycjaDokumentuDto.Ilosc;
            await pozycjaRepository.UpdateAsync(istniejącaPozycja);
            return pozycjaDokumentuDto;
        }
    }
}
