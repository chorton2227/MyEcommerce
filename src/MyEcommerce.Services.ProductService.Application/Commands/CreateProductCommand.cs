namespace MyEcommerce.Services.ProductService.Application.Commands
{
    using MyEcommerce.Core.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    public class CreateProductCommand : ICommand<ProductReadDto>
    {
        public ProductCreateDto ProductCreateDto { get; private set; }

        public CreateProductCommand(ProductCreateDto productCreateDto)
        {
            ProductCreateDto = productCreateDto;
        }
    }
}