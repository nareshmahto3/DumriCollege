using Admission.Api.DbConnection;
using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;

namespace Admission.Api.Infrastructures
{
    public class AdmissionRepository : Repository<Admission.Api.DbEntities.Admission, DumriCommerceCollegeContext>, IRepository<Admission.Api.DbEntities.Admission>
    {
        public AdmissionRepository(DumriCommerceCollegeContext context) : base(context)
        {
        }
    }
}
