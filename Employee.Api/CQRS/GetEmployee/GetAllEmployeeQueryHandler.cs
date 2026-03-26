using Employee.Api.DTOs;
using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Employee.Api.CQRS.GetEmployee
{
    public class GetAllEmployeeQueryHandler(IRepository<Employee.Api.DbEntities.Employee> _service) : IRequestHandler<GetAllEmployeeQuery, PagedResponse<EmployeeDto>>
    {
        public async Task<PagedResponse<EmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {

            var result = await _service.GetPagedAsync(null, request.Filter.PageNumber, request.Filter.PageSize);

            //var mapped = _mapper.Map<IEnumerable<AdmissionDto>>(result.Data);
            var mapped = result.Data.Select(a => new EmployeeDto
            {
                EmployeeId = a.EmployeeId,
                EmpId = a.EmpId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Gender = a.Gender,
                DateOfBirth = a.DateOfBirth,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                BloodGroup = a.BloodGroup,
                City = a.City,
                Department = a.Department,
                Designation = a.Designation,
                EmergencyContact = a.EmergencyContact,
                Experience = a.Experience,
                JoiningDate = a.JoiningDate,
                Qualification = a.Qualification,
                Religion = a.Religion,
                ShortBio = a.ShortBio,
                State = a.State,
                Subjects = a.Subjects,
                ZipCode = a.ZipCode,
                Salary = a.Salary,
                CreatedAt = a.CreatedAt

            });
            return new PagedResponse<EmployeeDto>(mapped, result.TotalRecords, request.Filter.PageNumber, request.Filter.PageSize);

        }
    }
}
