using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Services;

namespace Application.Services
{
    internal class DokumentPrzyjeciaService : IDokumentPrzyjeciaService
    {
        private readonly IDokumentRepository dokumentRepository;
        private readonly IPozycjaRepository pozycjaRepository;
        private readonly IKontrahentRepository kontrahentRepository;
        private readonly ITowarServices towarServices;

        public DokumentPrzyjeciaService(
            IDokumentRepository dokumentRepository, 
            IPozycjaRepository pozycjaRepository, 
            IKontrahentRepository kontrahentRepository,
            ITowarServices towarServices)
        {
            this.dokumentRepository = dokumentRepository;
            this.pozycjaRepository = pozycjaRepository;
            this.kontrahentRepository = kontrahentRepository;
            this.towarServices = towarServices;
        }

        public async Task<int> Create(CreateDokumentPrzyjeciaDto dokumentPrzyjecia)
        {
            // Sprawdź, czy kontrahent istnieje
            var kontrahent = await kontrahentRepository.GetByIdAsync(dokumentPrzyjecia.KontrahentId);
            if (kontrahent == null)
                throw new Exception("Kontrahent not exist");
            var towar = await towarServices.GetByIdAsync(dokumentPrzyjecia.TowarId);
            if (towar == null)
                throw new Exception("Towar not exist");

            // Tworzenie encji
            var dokument = new DokumentPrzyjecia
            {
                Data = DateTime.Now,
                Symbol = dokumentPrzyjecia.Symbol,
                KontrahentId = dokumentPrzyjecia.KontrahentId,
                Kontrahent = kontrahent,
                Pozycje = new List<PozycjaDokumentu>
                {
                    new PozycjaDokumentu
                    {
                        TowarId = dokumentPrzyjecia.TowarId,
                        Ilosc = dokumentPrzyjecia.Ilosc,
                    }
                }
            };

            return await dokumentRepository.CreateAsync(dokument);

        }

        public async Task<List<DokumentPrzyjeciaDto>> GetAllAsync()
        {
            var dokumentPrzyjecia= (await dokumentRepository.GetAllAsync()).ToList();
            var dtoList = dokumentPrzyjecia.Select(d => new DokumentPrzyjeciaDto
            {
                Id = d.Id,
                Symbol = d.Symbol,
                Data = d.Data,
                Kontrahent = new KontrahentDto
                {
                    Id = d.Kontrahent.Id,
                    Nazwa = d.Kontrahent.Nazwa,
                    Symbol = d.Kontrahent.Symbol
                },
                Pozycje = d.Pozycje.Select( p => new PozycjaDokumentuDto { 
                    Id = p.Id,
                    TowarId = p.TowarId,
                    NazwaTowaru = p.Towar.NazwaTowaru,
                    JednostkaMiary = p.Towar.JednostkaMiary,
                    Ilosc = p.Ilosc})
                .ToList()
                
            }).ToList();
            return dtoList;
        }

        public async Task<DokumentPrzyjeciaDto> GetByIdAsync(int id)
        {
            var dokumentPrzyjecia = await dokumentRepository.GetByIdAsync(id);

            if (dokumentPrzyjecia == null) {
                throw new Exception("Dokument przyjecia doesnt exist");
            }

            return new DokumentPrzyjeciaDto
            {
                Id = dokumentPrzyjecia.Id,
                Data = dokumentPrzyjecia.Data,
                Symbol = dokumentPrzyjecia.Symbol,
                Kontrahent = new KontrahentDto
                {
                    Id = dokumentPrzyjecia.KontrahentId,
                    Symbol = dokumentPrzyjecia.Kontrahent.Symbol,
                    Nazwa = dokumentPrzyjecia.Kontrahent.Nazwa,
                },
                Pozycje = dokumentPrzyjecia.Pozycje.Select(p => new PozycjaDokumentuDto
                {
                    Id = p.Id,
                    TowarId = p.TowarId,
                    NazwaTowaru = p.Towar.NazwaTowaru,
                    JednostkaMiary = p.Towar.JednostkaMiary,
                    Ilosc = p.Ilosc
                })
                .ToList()
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await dokumentRepository.DeleteAsync(id);
        }
        public async Task<DokumentPrzyjeciaDto?> UpdateAsync(UpdateDokumentPrzyjeciaDto dokumentPrzyjeciaDto, int dokumentPrzyjeciaID)
        {
            var dokument = await dokumentRepository.GetByIdAsync(dokumentPrzyjeciaID);
            if (dokument == null)
                return null;

            dokument.Symbol = dokumentPrzyjeciaDto.Symbol;

            var kontrahent = await kontrahentRepository.GetByIdAsync(dokumentPrzyjeciaDto.KontrahentId);
            if (kontrahent == null)
                return null;

            dokument.KontrahentId = kontrahent.Id;
            var updatedDoc = await dokumentRepository.UpdateAsync(dokument);
            if (updatedDoc == null) return null;
            
            return new DokumentPrzyjeciaDto { 
                Data = updatedDoc.Data,
                Symbol = dokument.Symbol,
                Kontrahent = new KontrahentDto
                {
                    Id =updatedDoc.Kontrahent.Id,
                },
                Pozycje = updatedDoc.Pozycje.Select(p => new PozycjaDokumentuDto
                {
                    Id = p.Id,
                    NazwaTowaru = p.Towar.NazwaTowaru,
                    JednostkaMiary = p.Towar.JednostkaMiary,
                    Ilosc = p.Ilosc,
                    TowarId = p.TowarId,
                })
                .ToList()
            };
        }
    }
}
