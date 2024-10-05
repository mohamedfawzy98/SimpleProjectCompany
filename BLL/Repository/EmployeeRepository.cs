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
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetNameAsync(string name)
        {
           return await _context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.WorkFor).ToListAsync();
        }

        



        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.Include(E => E.WorkFor).ToList();
        //}

        //public Employee GetId(int? id)
        //{
        //    return _context.Employees.Find(id);
        //}


        //public int Add(Employee entity)
        //{
        //    _context.Employees.Add(entity);
        //    return _context.SaveChanges();
        //}

        //public int Update(Employee entity)
        //{
        //    _context.Employees.Update(entity);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee entity)
        //{
        //    _context.Employees.Remove(entity);
        //    return _context.SaveChanges();
        //}


    }
}
