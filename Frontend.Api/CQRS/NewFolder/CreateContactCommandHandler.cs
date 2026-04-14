using Frontend.Api.DbConnection;
using Frontend.Api.DbEntities;
using MediatR;

namespace Frontend.Api.CQRS.NewFolder
{
    public class CreateContactCommandHandler
    {
        public class CreateContactHandler : IRequestHandler<CreateContactCommand, int>
        {
            private readonly DumriCollegeDbContext _context;

            public CreateContactHandler(DumriCollegeDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
            {
                var contact = new Contact
                {
                    FirstName = request.Contact.FirstName,
                    LastName = request.Contact.LastName,
                    Email = request.Contact.Email,
                    PhoneNumber = request.Contact.PhoneNumber,
                    Subject = request.Contact.Subject,
                    Message = request.Contact.Message
                };

                // ✅ Add entity
                await _context.Contacts.AddAsync(contact);

                // ✅ Save to DB
                await _context.SaveChangesAsync();

                // ✅ Return Id
                return contact.Id;
            }
        }
    }
}