using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Api.CQRS.Command;
using User.Api.DbConnection;
using User.Api.DTOs;
using static User.Api.CQRS.CommandHandler.MasterCommandHandler;

namespace User.Api.CQRS.CommandHandler
{
    public class MasterCommandHandler : IRequestHandler<MasterCommand, List<DropdownItem>>
    {
        private readonly DumriCollegeDbContext _context;

        public MasterCommandHandler(
            IUnitOfWork unitOfWork,
            DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownItem>> Handle(MasterCommand request, CancellationToken cancellationToken)
        {
            return await GetTableDataAsync(request.TableName);
        }


        public async Task<List<DropdownItem>> GetTableDataAsync(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required");

            tableName = tableName.Trim().ToLower();

            return tableName switch
            {
                "m_academicyear" => await _context.MAcademicYears
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.YearName })
                                    .ToListAsync(),

                "m_category" => await _context.MCategories
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.CategoryId, Name = x.CategoryName })
                                    .ToListAsync(),

                "m_classes" => await _context.MClasses
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.ClassId, Name = x.ClassName })
                                    .ToListAsync(),

                "m_examtype" => await _context.MExamTypes
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.ExamId, Name = x.ExamName })
                                    .ToListAsync(),

                "m_priority" => await _context.MPriorities
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.PriorityId, Name = x.PriorityName })
                                    .ToListAsync(),

                "m_targetaudience" => await _context.MTargetAudiences
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.AudienceName })
                                    .ToListAsync(),

                "m_gender" => await _context.MGenders
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.Gender })
                                    .ToListAsync(),

                "m_bloodgroup" => await _context.MBloodGroups
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.BloodGroup })
                                    .ToListAsync(),

                "m_religion" => await _context.MReligions
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.Name })
                                    .ToListAsync(),

                "m_designation" => await _context.MDesignations
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.DesigName })
                                    .ToListAsync(),

                "m_department" => await _context.MDepartments
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.DepartmentName })
                                    .ToListAsync(),

                "m_qualifications" => await _context.MQualifications
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.QualificationName })
                                    .ToListAsync(),

                "m_teachingsubjects" => await _context.MTeachingSubjects
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.SubjectName })
                                    .ToListAsync(),

                "m_state" => await _context.MStates
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.StateName })
                                    .ToListAsync(),

                "m_city" => await _context.MCities
                                    .AsNoTracking()
                                    .Select(x => new DropdownItem { Id = x.Id, Name = x.DistrictName })
                                    .ToListAsync(),

                _ => throw new KeyNotFoundException($"Table '{tableName}' is not supported.")
            };
        }
        public class DropdownItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
