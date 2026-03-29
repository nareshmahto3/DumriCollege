using MediatR;
using User.Api.Infrastructures;

namespace User.Api.CQRS.Command
{
    public class MasterCommandHandler : IRequestHandler<MasterCommand, object>
    {
        private readonly IDynamicRepository _repository;

        public MasterCommandHandler(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(MasterCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetTableDataAsync(request.TableName);
        }
    }
}