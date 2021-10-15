namespace MyEcommerce.Services.ProductService.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    public interface IProductApplication : IApplication
    {
        Task<ProductReadDto> CreateProduct(ProductCreateCommand command);

        ProductReadDto GetProductById(string id);

        PaginatedProductsDto GetProducts(ProductOptionsDto optionsDto);

        IEnumerable<CategoryReadDto> GetCategories(string catalogId);
    }
}