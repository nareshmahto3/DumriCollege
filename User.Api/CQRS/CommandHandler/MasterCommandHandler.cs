using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Api.CQRS.Command;
using User.Api.DbConnection;
using User.Api.DTOs;

namespace User.Api.CQRS.CommandHandler
{
    public class MasterCommandHandler : IRequestHandler<MasterCommand, object>
    {
        private readonly DumriCollegeDbContext _context;

        public MasterCommandHandler(
            IUnitOfWork unitOfWork,
            DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(MasterCommand request, CancellationToken cancellationToken)
        {
            return await GetTableDataAsync(request.TableName);
        }
    

        public async Task<object> GetTableDataAsync(string tableName)
        {
            // ✅ Validation
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required");

            // Normalize input
            tableName = tableName.Trim().ToLower();

            try
            {
                return tableName switch
                {
                    // ================= MAIN TABLES =================
                    //"users" => await _context.Users.AsNoTracking().ToListAsync(),
                    //"roles" => await _context.MRoles.AsNoTracking().ToListAsync(),
                    //"admissions" => await _context.Admissions.AsNoTracking().ToListAsync(),
                    //"userroles" => await _context.UserRoles.AsNoTracking().ToListAsync(),

                    // ================= MASTER TABLES =================
                    "m_academicyear" => await _context.MAcademicYears.AsNoTracking().ToListAsync(),
                    "m_category" => await _context.MCategories.AsNoTracking().ToListAsync(),
                    "m_classes" => await _context.MClasses.AsNoTracking().ToListAsync(),
                    "m_examType" => await _context.MExamTypes.AsNoTracking().ToListAsync(),
                    "m_priority" => await _context.MPriorities.AsNoTracking().ToListAsync(),
                    "m_targetAudience" => await _context.MTargetAudiences.AsNoTracking().ToListAsync(),
                    "m_gender" => await _context.MGenders.AsNoTracking().ToListAsync(),


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
