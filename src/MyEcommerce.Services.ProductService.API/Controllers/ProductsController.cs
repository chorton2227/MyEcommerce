namespace MyEcommerce.Services.ProductService.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyEcommerce.Services.ProductService.Application;
    using MyEcommerce.Services.ProductService.Application.Commands;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly IMediator _mediator;
        
        private readonly IProductApplication _productApplication;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator, IProductApplication productApplication)
        {
            _logger = logger;
            _mediator = mediator;
            _productApplication = productApplication;
        }

        [HttpGet(Name=nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<PaginatedProductsDto> GetAll(int page, int limit)
        {
            var products = _productApplication.GetProducts(page, limit);
            return Ok(products);
        }

        [HttpGet("{id}", Name=nameof(GetById))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<ProductReadDto> GetById(string id)
        {
            var product = _productApplication.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost(Name=nameof(CreateAsync))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductReadDto>> CreateAsync(
            [FromBody] ProductCreateDto productCreateDto)
        {
            var command = new ProductCreateCommand(productCreateDto);
            var product = await _productApplication.CreateProduct(command);
            return CreatedAtRoute(nameof(GetById), new { Id = product.Id }, product);
        }
    }
}