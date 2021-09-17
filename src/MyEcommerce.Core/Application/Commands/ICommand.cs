namespace MyEcommerce.Core.Application.Commands
{
    using MediatR;

    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}