using User.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Infrastructures
{



}    }        }        {        public StudentDocumentRepository(DumriCommerceCollegeContext context) : base(context)    {    public class StudentDocumentRepository : Repository<User.Api.DbEntities.StudentDocument, DumriCommerceCollegeContext>, IRepository<User.Api.DbEntities.StudentDocument>{namespace User.Api.Infrastructuresusing LibraryService.Utility.Data.Core.Repositories;using LibraryService.Utility.Data.Core.Interfaces;using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace User.Api.Infrastructures
{
    public class StudentDocumentRepository : Repository<User.Api.DbEntities.StudentDocument, DumriCommerceCollegeContext>, IRepository<User.Api.DbEntities.StudentDocument>
    {
        public StudentDocumentRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}
