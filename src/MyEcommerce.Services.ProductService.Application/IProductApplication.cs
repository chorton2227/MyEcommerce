namespace MyEcommerce.Services.ProductService.Application
{
    using System.Threading.Tasks;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    public interface IProductApplication : IApplication
    {
        Task<ProductReadDto> CreateProduct(ProductCreateCommand command);

        ProductReadDto GetProductById(string id);

        PaginatedProductsDto GetProducts(int page, int limit);
    }
}