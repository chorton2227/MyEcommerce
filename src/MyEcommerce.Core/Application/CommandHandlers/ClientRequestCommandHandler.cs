namespace MyEcommerce.Core.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using MyEcommerce.Core.Application.Commands;
    using MyEcommerce.Core.Domain.AggregateModels.ClientRequestAggregate;

    public class ClientRequestCommandHandler<TCommand, TResponse>
        : ICommandHandler<ClientRequestCommand<TCommand, TResponse>, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IMediator _mediator;

        private readonly IClientRequestRepository _clientRequestRepository;

        public ClientRequestCommandHandler(IMediator mediator, IClientRequestRepository clientRequestRepository)
        {
            _mediator = mediator;
            _clientRequestRepository = clientRequestRepository;
        }

        public async Task<TResponse> Handle(ClientRequestCommand<TCommand, TResponse> request, CancellationToken cancellationToken)
        {
            try
            {
                var clientRequestId = new ClientRequestId(request.RequestId);
                if (await _clientRequestRepository.ExistsAsync(clientRequestId).ConfigureAwait(false))
                {
                    return HandleDuplicateRequest();
                }

                _ = await _clientRequestRepository.CreateAsync(clientRequestId).ConfigureAwait(false);
                return await _mediator.Send(request.Command, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                // TODO: log exception
                return HandleDuplicateRequest();
            }
        }

        protected virtual TResponse HandleDuplicateRequest()
        {
            return default;
        }
    }
}