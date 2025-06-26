using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    internal class KontrahentRepository : IKontrahentRepository
    {
        private readonly WarehouseContext _context;

        public KontrahentRepository(WarehouseContext context)
        {
            _context = context;
        }

        /// <returns>Returns list of kontrahents or empty list</returns>
        public async Task<IEnumerable<Kontrahent>> GetAllAsync()
        {
            return await _context.Kontrahenci.ToListAsync();
        }


        /// <param name="id"></param>
        /// <returns>Return kontrahent of choosen id or null if doesn't exist</returns>
        public async Task<Kontrahent?> GetByIdAsync(int id)
        {
            var k = await _context.Kontrahenci.FindAsync(id);

            return k;
        }


        /// <param name="dto"></param>
        /// <returns>Returns ID of new kontrahent</returns>
        public async Task<int> CreateAsync(Kontrahent kontrahent)
        {

            _context.Kontrahenci.Add(kontrahent);
            await _context.SaveChangesAsync();
            return kontrahent.Id;
        }
        public async Task<int> UpdateAsync(Kontrahent kontrahent)
        {

            _context.Kontrahenci.Update(kontrahent);
            await _context.SaveChangesAsync();
            return kontrahent.Id;
        }


        /// <param name="id"></param>
        /// <returns>Return true if deleted or return false if not (kontrahent doesn't exist or have dokumentPrzyjecia assigned to himself</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var k = await _context.Kontrahenci
                .FirstOrDefaultAsync(k => k.Id == id);

            //if (k == null || (k.DokumentyPrzyjecia ?? Enumerable.Empty<DokumentPrzyjecia>()).Any())
            //throw new Exception("Kontrahent does not exist or has Dokument przyjecia assigned to them.");
            if (k == null || k.DokumentyPrzyjecia.Any())
                return false;

            _context.Kontrahenci.Remove(k);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
