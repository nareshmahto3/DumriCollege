using Employee.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Infrastructures
{
    public class EmployeeRepository : Repository<Employee.Api.DbEntities.Employee, EmployeeDbContext>, IRepository<Employee.Api.DbEntities.Employee>
    {
        public EmployeeRepository(EmployeeDbContext context) : base(context)
    {
    }

     

public async Task<List<Employee.Api.DbEntities.Employee>> GetEmployee()
    {
        return await _context.Set<Employee.Api.DbEntities.Employee>().ToListAsync();
    }
}
}
    
