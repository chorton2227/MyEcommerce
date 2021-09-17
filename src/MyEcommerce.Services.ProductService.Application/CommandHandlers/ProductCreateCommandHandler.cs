namespace MyEcommerce.Services.ProductService.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MyEcommerce.Core.Application.CommandHandlers;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductCreateCommandHandler : ICommandHandler<ProductCreateCommand, ProductReadDto>
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductCreateCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                new ProductType("todo"),
                new ProductBrand("todo"),
                request.ProductCreateDto.Name,
                request.ProductCreateDto.Description,
                request.ProductCreateDto.Price,
                request.ProductCreateDto.ImageFileName,
                request.ProductCreateDto.ImageUri,
                request.ProductCreateDto.AvailableStock,
                request.ProductCreateDto.RestockTreshold,
                request.ProductCreateDto.MaxStockThreshold,
                request.ProductCreateDto.OnReorder
            );

            _productRepository.Create(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }
    }
}