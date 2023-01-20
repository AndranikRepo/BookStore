
using BookStore.Infrastructure.Interfaces;
using BookStore.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetAsync(id, cancellationToken));
        }

        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> Get(int skip, int take, CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetAsync(skip, take, cancellationToken));
        }

        [HttpPost]
        public async Task Post([FromBody] CreateBookDto createBookDto, CancellationToken cancellationToken)
        {
            await _bookService.AddAsync(createBookDto, cancellationToken);
        }

        [HttpPut]
        public async Task Put([FromBody] UpdateBookDto updateBookDto, CancellationToken cancellationToken)
        {
            await _bookService.UpdateAsync(updateBookDto, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bookService.DeleteAsync(id, cancellationToken);
        }
    }
}
