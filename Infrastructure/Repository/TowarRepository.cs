using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal class TowarRepository : ITowarRepository
    {
        private readonly WarehouseContext _context;

        public TowarRepository(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<List<Towar>> GetAllAsync()
        {
            return await _context.Towar.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Towar or null if doesnt exist</returns>
        public async Task<Towar?> GetByIdAsync(int id)
        {
            var p = await _context.Towar.FindAsync(id);

            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Towar">object to update</param>
        /// <returns>Returns true if updated</returns>
        public async Task<bool> UpdateAsync(Towar Towar)
        {
            var p = await _context.Towar.FindAsync(Towar.Id);
            if (p == null) return false;

            p.NazwaTowaru = Towar.NazwaTowaru;
            p.JednostkaMiary = Towar.JednostkaMiary;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Towar.FindAsync(id);
            if (p == null) return false;

            _context.Towar.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Towar?> CreateAsync(Towar towar)
        {
            if (towar == null)
                return null;

            var created = (await _context.Towar.AddAsync(towar)).Entity;
            await _context.SaveChangesAsync();
            return created;
        }

    }
}
