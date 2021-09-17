namespace MyEcommerce.Services.ProductService.Application
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    public class ProductApplication : BaseApplication, IProductApplication
    {
        public ProductApplication(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ProductReadDto> CreateProduct(CreateProductCommand command, Guid requestId)
        {
            return await Handle(command, requestId).ConfigureAwait(false);
        }
    }
}