using Employee.Api.DTOs;
using Employee.Api.DbEntities;

using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

namespace Employee.Api.CQRS.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler
        : IRequestHandler<UpdateEmployeeCommand, ResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeRepo = _unitOfWork.Repository<Employee.Api.DbEntities.Employee>();


            var existingEmployee = await employeeRepo.GetByIdAsync(request.EmployeeDto.EmployeeId);

            if (existingEmployee == null)
                return ResponseDto.Fail("No record found");

            // ❌ Never update primary key
            // existingEmployee.EmployeeId = request.EmployeeDto.EmployeeId;

            existingEmployee.EmpId = request.EmployeeDto.EmpId;
            existingEmployee.FirstName = request.EmployeeDto.FirstName;
            existingEmployee.LastName = request.EmployeeDto.LastName;
            existingEmployee.Gender = request.EmployeeDto.Gender;
            existingEmployee.DateOfBirth = request.EmployeeDto.DateOfBirth;
            existingEmployee.Email = request.EmployeeDto.Email;
            existingEmployee.PhoneNumber = request.EmployeeDto.PhoneNumber;
            existingEmployee.Address = request.EmployeeDto.Address;
            existingEmployee.BloodGroup = request.EmployeeDto.BloodGroup;
            existingEmployee.City = request.EmployeeDto.City;
            existingEmployee.Department = request.EmployeeDto.Department;
            existingEmployee.Designation = request.EmployeeDto.Designation;
            existingEmployee.EmergencyContact = request.EmployeeDto.EmergencyContact;
            existingEmployee.Experience = request.EmployeeDto.Experience;
            existingEmployee.JoiningDate = request.EmployeeDto.JoiningDate;
            existingEmployee.Qualification = request.EmployeeDto.Qualification;
            existingEmployee.Religion = request.EmployeeDto.Religion;
            existingEmployee.ShortBio = request.EmployeeDto.ShortBio;
            existingEmployee.State = request.EmployeeDto.State;
            existingEmployee.Subjects = request.EmployeeDto.Subjects;
            existingEmployee.ZipCode = request.EmployeeDto.ZipCode;
            existingEmployee.Salary = request.EmployeeDto.Salary;

            employeeRepo.Update(existingEmployee);

         //   await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseDto.Success(existingEmployee, "Employee updated successfully");
        }
    }
}
