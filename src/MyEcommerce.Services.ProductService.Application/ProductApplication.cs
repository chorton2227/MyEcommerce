namespace MyEcommerce.Services.ProductService.Application
{
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

        public async Task<ProductReadDto> CreateProduct(ProductCreateCommand command)
        {
            return await Handle(command).ConfigureAwait(false);
        }
    }
}