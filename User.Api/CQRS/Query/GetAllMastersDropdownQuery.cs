using LibraryService.Utility.Data.Core.DTOs;
using MediatR;
using User.Api.DTOs;

namespace User.Api.CQRS.Query
{

    public class GetAllMastersDropdownQuery : IRequest<List<DropdownDto>>
    {
        public string TableName { get; set; }

        public GetAllMastersDropdownQuery(string tableName)
        {
            TableName = tableName;
        }
    }
}