using System.Collections.Generic;
using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstWebApiSite.Controllers
{
    public class CategoriesController : Controller
    {
       
        private ICategoryService _categoryService;
        private IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            try
            {
                List<MyFirstWebApiSite.Category> categories = await _categoryService.GetCategories();
                List<CategoryDto> categoriesDto= _mapper.Map < List <Category>, List <CategoryDto>> (categories);
                return Ok(categories);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
