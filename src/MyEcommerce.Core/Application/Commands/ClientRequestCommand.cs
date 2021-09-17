namespace MyEcommerce.Core.Application.Commands
{
    using System;

    public class ClientRequestCommand<TCommand, TResponse> : ICommand<TResponse>
        where TCommand : ICommand<TResponse>
    {
        public TCommand Command { get; protected set; }

        public Guid RequestId { get; protected set; }

        public ClientRequestCommand(TCommand command, Guid requestId)
        {
            Command = command;
            RequestId = RequestId;
        }
    }
}