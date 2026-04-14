using Employee.Api.DTOs;
using MediatR;

namespace Employee.Api.CQRS.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<ResponseDto>

    {
        public int EmployeeId { get; }

        public DeleteEmployeeCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
   