namespace MyEcommerce.Services.ProductService.Application
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductApplication : BaseApplication, IProductApplication
    {
        private const int DEFAULT_PAGE = 0;

        private const int DEFAULT_LIMIT = 12;

        private const int MAX_LIMIT = 50;

        private readonly IMapper _mapper;
        
        private readonly IProductRepository _productRepository;

        public ProductApplication(IMediator mediator, IMapper mapper, IProductRepository productRepository)
            : base(mediator)
        {
            _mapper = mapper;
            _productRepository = productRepository;
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

        public PaginatedProductsDto GetProducts(int page, int limit)
        {
            if (page < 0) {
                page = DEFAULT_LIMIT;
            }

            if (limit < 0 || limit > MAX_LIMIT) {
                limit = DEFAULT_LIMIT;
            }

            var realLimit = limit + 1;
            var products = _productRepository.GetAll(page, realLimit);

            var hasMore = products.Count() == realLimit;
            var productDtos = _mapper.Map<IEnumerable<ProductReadDto>>(products.Take(limit));
            return new PaginatedProductsDto
            {
                HasMore = hasMore,
                Products = productDtos
            };
        }
    }
}