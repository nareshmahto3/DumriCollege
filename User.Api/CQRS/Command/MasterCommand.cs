using MediatR;
using User.Api.DTOs;
using static User.Api.CQRS.CommandHandler.MasterCommandHandler;

namespace User.Api.CQRS.Command
{
    public class MasterCommand : IRequest<List<DropdownItem>>
    {
        public string TableName { get; set; }

        public MasterCommand(string tableName)
        {
            TableName = tableName;
        }
    }
}