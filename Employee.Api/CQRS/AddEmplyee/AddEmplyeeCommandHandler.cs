using Employee.Api.CQRS.AddEmplyee;
using Employee.Api.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;

public class AddEmplyeeCommandHandler
    : IRequestHandler<AddEmplyeeCommand, ResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddEmplyeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> Handle(AddEmplyeeCommand request, CancellationToken cancellationToken)
    {
        if (request.EmployeeDto == null)
            return ResponseDto.Fail("Employee data is null");

        var employee = new Employee.Api.DbEntities.Employee
        {
            EmpId = request.EmployeeDto.EmpId,
            FirstName = request.EmployeeDto.FirstName,
            LastName = request.EmployeeDto.LastName,
            Gender = request.EmployeeDto.Gender,
            DateOfBirth = request.EmployeeDto.DateOfBirth,
            Email = request.EmployeeDto.Email,
            PhoneNumber = request.EmployeeDto.PhoneNumber,
            Address = request.EmployeeDto.Address,
            BloodGroup = request.EmployeeDto.BloodGroup,
            City = request.EmployeeDto.City,
            Department = request.EmployeeDto.Department,
            Designation = request.EmployeeDto.Designation,
            EmergencyContact = request.EmployeeDto.EmergencyContact,
            Experience = request.EmployeeDto.Experience,
            JoiningDate = request.EmployeeDto.JoiningDate,
            Qualification = request.EmployeeDto.Qualification,
            Religion = request.EmployeeDto.Religion,
            ShortBio = request.EmployeeDto.ShortBio,
            State = request.EmployeeDto.State,
            Subjects = request.EmployeeDto.Subjects,
            ZipCode = request.EmployeeDto.ZipCode,
            Salary = request.EmployeeDto.Salary,
            CreatedAt = DateTime.UtcNow
        };

        var employeeRepo = _unitOfWork.Repository<Employee.Api.DbEntities.Employee>();

        await employeeRepo.AddAsync(employee);

      //  await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseDto.Success(null, "Employee added successfully");
    }
}
