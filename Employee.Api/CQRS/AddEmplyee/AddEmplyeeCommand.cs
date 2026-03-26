using Employee.Api.DTOs;
using MediatR;

namespace Employee.Api.CQRS.AddEmplyee
{
    public class AddEmplyeeCommand  : IRequest<ResponseDto>
    {
        public EmployeeDto EmployeeDto { get; set; }

        public AddEmplyeeCommand(EmployeeDto admissionDto)
        {
            EmployeeDto = admissionDto;
        }
    }

    

    }

