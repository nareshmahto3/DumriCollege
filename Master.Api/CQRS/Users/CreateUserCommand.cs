namespace Master.Api.CQRS.Users
{
    using MediatR;
    using Master.Api.DTOs;

    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDto User { get; set; }

        public CreateUserCommand(CreateUserDto user)
        {
            User = user;
        }
    }
}
