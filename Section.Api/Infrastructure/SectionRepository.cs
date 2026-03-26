using LibraryService.Utility.Data.Core.Interfaces;
using LibraryService.Utility.Data.Core.Repositories;
using Section.Api.DbConnection;

namespace Section.Api.Infrastructure
{
    public class SectionRepository : Repository<Section.Api.DbEntities.Section, SchoolDbContext>, IRepository<Section.Api.DbEntities.Section>
    {
        public SectionRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}

