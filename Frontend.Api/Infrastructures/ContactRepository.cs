using Frontend.Api.DbConnection;
using Frontend.Api.DbEntities;

namespace Frontend.Api.Infrastructures
{
    public class ContactRepository
    {
        private readonly DumriCollegeDbContext _context;

        public ContactRepository(DumriCollegeDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }
    }
}
