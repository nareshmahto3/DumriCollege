using Employee.Api.DTOs;
using MediatR;

namespace Employee.Api.CQRS.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<ResponseDto>
    {
        public EmployeeDto EmployeeDto { get; set; }

        public UpdateEmployeeCommand(EmployeeDto employeeDto)
        {
            EmployeeDto = employeeDto;
        }
    }
}
