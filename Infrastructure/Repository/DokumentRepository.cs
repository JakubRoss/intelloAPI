using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    internal class DokumentRepository : IDokumentRepository
    {
        private readonly WarehouseContext _context;

        public DokumentRepository(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DokumentPrzyjecia>> GetAllAsync()
        {
            var check = await _context.DokumentyPrzyjecia
                .Include(d => d.Kontrahent)
                .Include(d => d.Pozycje)
                .ThenInclude(d => d.Towar)
                .ToListAsync();
            return check;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Accepts ID of DokumentPrzyjecia</param>
        /// <returns>returns null if it does not exist, otherwise return object of DokumentPrzyjecia</returns>
        public async Task<DokumentPrzyjecia?> GetByIdAsync(int id)
        {
            var d = await _context.DokumentyPrzyjecia
                .Include(d => d.Kontrahent)
                .Include(d => d.Pozycje)
                .ThenInclude(d => d.Towar)
                .FirstOrDefaultAsync(d => d.Id == id);

            return d;
        }

        /// <returns>Returns ID of Dokument Przyjecia</returns>
        public async Task<int> CreateAsync(DokumentPrzyjecia dokumentPrzyjecia)
        {
            var dokument = new DokumentPrzyjecia
            {
                Data = dokumentPrzyjecia.Data,
                Symbol = dokumentPrzyjecia.Symbol,
                KontrahentId = dokumentPrzyjecia.KontrahentId,
                Pozycje = dokumentPrzyjecia.Pozycje.Select(p => new PozycjaDokumentu
                {
                    TowarId = p.TowarId,
                    Ilosc = p.Ilosc
                }).ToList()
            };

            _context.DokumentyPrzyjecia.Add(dokument);
            await _context.SaveChangesAsync();

            return dokument.Id;
        }


        /// <param name="id">Accepts ID of DokumentPrzyjecia</param>
        /// <returns>Return True if deleted</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var dokument = await _context.DokumentyPrzyjecia.FindAsync(id);
            if (dokument == null) return false;

            _context.DokumentyPrzyjecia.Remove(dokument);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DokumentPrzyjecia> UpdateAsync(DokumentPrzyjecia dokumentPrzyjecia)
        {
            _context.Update(dokumentPrzyjecia);
            await _context.SaveChangesAsync();
            return dokumentPrzyjecia;
        }
    }
}
