namespace Frontend.Api.CQRS.NewFolder
{
    using Frontend.Api.DTOs;
    using MediatR;

    public class CreateContactCommand : IRequest<int>
    {
        public ContactDto Contact { get; set; }

        public CreateContactCommand(ContactDto contact)
        {
            Contact = contact;
        }
    }
}
