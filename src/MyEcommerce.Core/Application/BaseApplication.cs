namespace MyEcommerce.Core.Application
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using MyEcommerce.Core.Application.Commands;

    public abstract class BaseApplication : IApplication
    {
        private readonly IMediator _mediator;

        public BaseApplication(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<TResponse> Handle<TResponse>(ICommand<TResponse> command, Guid requestId)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var clientRequestCommand = new ClientRequestCommand<ICommand<TResponse>, TResponse>(command, requestId);
            return await _mediator.Send(clientRequestCommand).ConfigureAwait(false);
        }
    }
}