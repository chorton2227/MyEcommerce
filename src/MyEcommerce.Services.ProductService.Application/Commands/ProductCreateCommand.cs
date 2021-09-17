namespace MyEcommerce.Services.ProductService.Application.Commands
{
    using MyEcommerce.Core.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    public class ProductCreateCommand : ICommand<ProductReadDto>
    {
        public ProductCreateDto ProductCreateDto { get; private set; }

        public ProductCreateCommand(ProductCreateDto productCreateDto)
        {
            ProductCreateDto = productCreateDto;
        }
    }
}