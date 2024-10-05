using BLL.interfaces;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T:BasicClass
    {
        private protected readonly AppDbContext _context;

        public GenaricRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>)await _context.Employees.Include(E => E.WorkFor).AsNoTracking().ToListAsync();
               //return (IEnumerable<T>)_context.Employees.Include(E => E.WorkFor).ToList();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }


        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        
    }
}
