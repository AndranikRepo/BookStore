using BookStore.Infrastructure.Interfaces;
using BookStore.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAsync(id, cancellationToken));
        }

        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> Get(int skip, int take, CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAsync(skip, take, cancellationToken));
        }

        [HttpPost]
        public async Task Post([FromBody] CreateAuthorDto createAuthorDto, CancellationToken cancellationToken)
        {
            await _authorService.AddAsync(createAuthorDto, cancellationToken);
        }

        [HttpPut]
        public async Task Put([FromBody] AuthorDto authorDto, CancellationToken cancellationToken)
        {
            await _authorService.UpdateAsync(authorDto, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _authorService.DeleteAsync(id, cancellationToken);
        }
    }
}
