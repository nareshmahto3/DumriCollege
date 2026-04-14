using MediatR;

namespace Master.Api.CQRS.Users
{

public class RoleMappingCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
