namespace MyEcommerce.Services.ProductService.Application
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<ProductReadDto> GetProducts()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}