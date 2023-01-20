using BookStore.Infrastructure.Interfaces;
using BookStore.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await _categoryService.GetAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task Post([FromBody] CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            await _categoryService.AddAsync(createCategoryDto, cancellationToken);
        }

        [HttpPut]
        public async Task Put([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateAsync(categoryDto, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
        }
    }
}
