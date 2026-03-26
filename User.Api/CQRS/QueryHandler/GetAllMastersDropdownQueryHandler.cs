using LibraryService.Utility.Data.Core.DTOs;
using LibraryService.Utility.Data.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using User.Api.CQRS.Query;
using User.Api.DbConnection;
using User.Api.DbEntities;
using User.Api.DTOs;

namespace User.Api.CQRS.QueryHandler
{

    public class GetAllMastersDropdownQueryHandler : IRequestHandler<GetAllMastersDropdownQuery, List<DropdownDto>>
    {
        private readonly DumriCommerceCollegeContext _context;

        public GetAllMastersDropdownQueryHandler(DumriCommerceCollegeContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownDto>> Handle(GetAllMastersDropdownQuery request, CancellationToken cancellationToken)
        {
            var tableName = request.TableName.ToLower();

            switch (tableName)
            {
                case "m_role":
                    return await _context.MRoles
                        .Select(x => new DropdownDto
                        {
                            Id = x.RoleId,
                            Name = x.RoleName
                        })
                        .ToListAsync(cancellationToken);

                //case "shifts":
                //    return await _context.Shifts
                //        .Where(x => x.IsActive)
                //        .Select(x => new DropdownDto
                //        {
                //            Id = x.ShiftId,
                //            Name = x.ShiftName
                //        })
                //        .ToListAsync(cancellationToken);

                default:
                    throw new Exception("Invalid table name");
            }
        }
    }
}
