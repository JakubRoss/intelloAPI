using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    internal class PozycjaRepository : IPozycjaRepository
    {
        private readonly WarehouseContext _context;

        public PozycjaRepository(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<List<PozycjaDokumentu>> GetAllAsync()
        {
            return await _context.PozycjeDokumentow.Include( p =>p.Towar).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return PozycjaDokumentu or null if doesnt exist</returns>
        public async Task<PozycjaDokumentu?> GetByIdAsync(int id)
        {
            return await _context.PozycjeDokumentow
                .Include(p => p.Towar)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PozycjaDokumentu?> UpdateAsync(PozycjaDokumentu pozycjaDokumentu)
        {
            var p = await _context.PozycjeDokumentow.FindAsync(pozycjaDokumentu.Id);
            if (p == null) return null;

            p.Towar.NazwaTowaru = pozycjaDokumentu.Towar.NazwaTowaru;
            p.Towar.JednostkaMiary = pozycjaDokumentu.Towar.JednostkaMiary;
            p.Ilosc = pozycjaDokumentu.Ilosc;

            
            await _context.SaveChangesAsync();
            return pozycjaDokumentu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.PozycjeDokumentow.FindAsync(id);
            if (p == null) return false;

            _context.PozycjeDokumentow.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PozycjaDokumentu?> CreateAsync(PozycjaDokumentu pozycjaDokumentu)
        {
            if(pozycjaDokumentu == null) 
                return null;
            var entry = await _context.PozycjeDokumentow.AddAsync(pozycjaDokumentu);
            var pozycja = entry.Entity;
            await _context.SaveChangesAsync();
            return pozycja;
        }
    }
}
