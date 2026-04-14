using Microsoft.EntityFrameworkCore;
using User.Api.DbConnection;
using User.Api.Infrastructures;

namespace User.Api.Infrastructures
{
    public class DynamicRepository : IDynamicRepository
    {
        private readonly DumriCommerceCollegeContext _context;

        public DynamicRepository(DumriCommerceCollegeContext context)
        {
            _context = context;
        }

        public async Task<object> GetTableDataAsync(string tableName)
        {
            // ✅ Validation
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required");

            // Normalize input
            tableName = tableName.Trim();

            try
            {
                return tableName switch
                {
                    // ================= MAIN TABLES =================
                    "users" => await _context.Users.AsNoTracking().ToListAsync(),
                    "roles" => await _context.Roles.AsNoTracking().ToListAsync(),
                    "admissions" => await _context.Admissions.AsNoTracking().ToListAsync(),
                    "userroles" => await _context.UserRoles.AsNoTracking().ToListAsync(),

                    // ================= MASTER TABLES =================
                    "M_AcademicYear" => await _context.MAcademicYears.AsNoTracking().ToListAsync(),
                    "M_Category" => await _context.MCategories.AsNoTracking().ToListAsync(),
                    "M_Classes" => await _context.MClasses.AsNoTracking().ToListAsync(),
                    "M_ExamType" => await _context.MExamTypes.AsNoTracking().ToListAsync(),
                    "M_Priority" => await _context.MPriorities.AsNoTracking().ToListAsync(),
                    "M_TargetAudience" => await _context.MTargetAudiences.AsNoTracking().ToListAsync(),

                    // ❌ Invalid table
                    _ => throw new KeyNotFoundException($"Table '{tableName}' is not supported.")
                };
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw new Exception($"Error fetching data from table '{tableName}': {ex.Message}");
            }
        }
    }
}