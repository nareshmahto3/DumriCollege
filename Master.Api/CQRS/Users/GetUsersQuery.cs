namespace Master.Api.CQRS.Users;
using MediatR;
using Master.Api.DTOs;
using System.Collections.Generic;

public class GetUsersQuery : IRequest<List<UserDto>>
{
}
