using MediatR;
using User.Api.DTOs;

namespace User.Api.CQRS.Command
{
    public class MasterCommand : IRequest<object>
    {
        public string TableName { get; set; }

        public MasterCommand(string tableName)
        {
            TableName = tableName;
        }
    }
}