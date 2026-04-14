namespace Master.Api.CQRS.Role
{
    using Master.Api.DTOs;
    using Master.Api.DTOs.Master.Api.DTOs;
    using MediatR;
    using System.Collections.Generic;

    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
