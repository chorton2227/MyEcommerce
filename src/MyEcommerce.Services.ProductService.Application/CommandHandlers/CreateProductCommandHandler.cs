namespace MyEcommerce.Services.ProductService.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MyEcommerce.Core.Application.CommandHandlers;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductReadDto>
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductCreateDto);
            _productRepository.Create(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }
    }
}