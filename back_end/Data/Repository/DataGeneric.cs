using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class DataGeneric<T> where T  : class
    {
        public readonly ApplicationDbContext _context;

        public DataGeneric(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() 
        { 
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T Entity) 
        {
            await _context.Set<T>().AddAsync(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        public async Task<bool> updateAsync(T Entity) 
        {
            _context.Set<T>().Update(Entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteAsync (int id) 
        {
            var Entity = await _context.Set<T>().FindAsync(id);
            if (Entity != null) return false;

            _context.Set<T>().Remove(Entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
