using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using User.Api.CQRS.Command;
using User.Api.DbEntities;
using User.Api.DTOs;

namespace User.Api.CQRS.CommandHandler
{
    public class AddRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            if (request.AdmissionDto == null)
                return ResponseDto.Fail("Role data is null");
            var role = new MRole
            {
                RoleName = request.AdmissionDto.RoleName

            };
            var productRepo = unitOfWork.Repository<MRole>();
            await productRepo.AddAsync(role);
            await unitOfWork.SaveChangesAsync();


            return ResponseDto.Success(Data: null, message: "Role added successfully");
        }
    }
}
