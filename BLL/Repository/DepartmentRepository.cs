using BLL.interfaces;
using DAL.Data.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context):base(context)
        {

        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetId(int? id)
        {
            return _context.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges();
        }
        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges(); ;
        }
        
    }
}
