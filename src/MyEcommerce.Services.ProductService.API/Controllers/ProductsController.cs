namespace MyEcommerce.Services.ProductService.API.Controllers
{
    using System;
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductReadDto>> CreateAsync(
            [FromBody] ProductCreateDto productCreateDto)
        {
            var command = new ProductCreateCommand(productCreateDto);
            return await _productApplication.CreateProduct(command);
            //return CreatedAtRoute(nameof(GetById), new { Id = result.Id }, result);
        }
    }
}