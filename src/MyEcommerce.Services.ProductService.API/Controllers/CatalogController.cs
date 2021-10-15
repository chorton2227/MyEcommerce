namespace MyEcommerce.Services.ProductService.API.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyEcommerce.Services.ProductService.Application;
    using MyEcommerce.Services.ProductService.Application.Dtos;

    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ILogger<CatalogsController> _logger;

        private readonly IMediator _mediator;
        
        private readonly IProductApplication _productApplication;

        public CatalogsController(ILogger<CatalogsController> logger, IMediator mediator, IProductApplication productApplication)
        {
            _logger = logger;
            _mediator = mediator;
            _productApplication = productApplication;
        }

        [HttpGet("{catalogId}/categories", Name=nameof(GetCategories))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<CategoryReadDto>> GetCategories(string catalogId)
        {
            var categories = _productApplication.GetCategories(catalogId);
            return Ok(categories);
        }
    }
}