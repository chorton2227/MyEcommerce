namespace MyEcommerce.Services.ProductService.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductApplication : BaseApplication, IProductApplication
    {
        private readonly IMapper _mapper;
        
        private readonly IProductRepository _productRepository;
        
        private readonly ICatalogRepository _catalogRepository;

        public ProductApplication(
            IMediator mediator,
            IMapper mapper,
            IProductRepository productRepository,
            ICatalogRepository catalogRepository
        ) : base(mediator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;
        }

        public async Task<ProductReadDto> CreateProduct(ProductCreateCommand command)
        {
            return await Handle(command).ConfigureAwait(false);
        }

        public ProductReadDto GetProductById(string id)
        {
            var product = _productRepository.GetById(new ProductId(id));
            return _mapper.Map<ProductReadDto>(product);
        }

        public PaginatedProductsDto GetProducts(ProductOptionsDto optionsDto)
        {
            var options = _mapper.Map<ProductOptions>(optionsDto);

            // Get 1 more for hasMore
            options.CheckForMoreRecords = true;

            // Get products, check if hasMore, and remove the extra product
            var paginatedProducts = _productRepository.GetAll(options);
            var hasMore = paginatedProducts.Products.Count() > options.PageLimit;
            if (hasMore) {
                paginatedProducts.Products = paginatedProducts.Products
                    .Take(options.PageLimit)
                    .ToList();
            }

            // Map and return dto
            var paginatedProductsDto = _mapper.Map<PaginatedProductsDto>(paginatedProducts);
            paginatedProductsDto.HasMore = hasMore;
            return paginatedProductsDto;
        }

        public IEnumerable<CategoryReadDto> GetCategories(string catalogId)
        {
            var categories = _catalogRepository.GetCategoriesByCatalogId(new CatalogId(catalogId));
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }
    }
}