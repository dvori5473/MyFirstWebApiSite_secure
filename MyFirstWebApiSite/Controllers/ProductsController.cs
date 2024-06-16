using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstWebApiSite.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        private IProductService _productService;
        private IMapper _mapper;
        public ProductsController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery]int?[] categories, [FromQuery] string ?descrebtion)
        {
            try
            {
                List<MyFirstWebApiSite.Product> products = await _productService.GetProducts(minPrice, maxPrice, categories, descrebtion); ;
                List<ProductDto> producrDto = _mapper.Map<List<Product>, List<ProductDto>>(products);
                return Ok(producrDto);
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
