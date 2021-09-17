namespace MyEcommerce.Core.Application.CommandHandlers
{
    using MediatR;
    using MyEcommerce.Core.Application.Commands;

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}