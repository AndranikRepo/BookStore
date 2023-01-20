using AutoMapper;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using BookStore.Infrastructure.Interfaces;
using BookStore.Shared;
using BookStore.Shared.Dto;

namespace BookStore.Infrastructure.Classes
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _genericRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IGenericRepository<Author> genericRepository,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _genericRepository = genericRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDto?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _genericRepository.FindByIdAsync(cancellationToken, id);

            return author != null
                ? _mapper.Map<Author, AuthorDto>(author)
                : null;
        }

        public async Task<IEnumerable<AuthorDto>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(await _authorRepository.GetAsync(skip, take, cancellationToken));
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(await _genericRepository.GetAllAsync(x => !x.IsRemoved, cancellationToken));
        }

        public async Task<AuthorDto?> FindByIdAsync(CancellationToken cancellationToken, params object?[]? id)
        {
            var author = await _genericRepository.FindByIdAsync(cancellationToken, id);

            return author != null
                ? _mapper.Map<Author, AuthorDto>(author)
                : null;
        }

        public async Task AddAsync(CreateAuthorDto createAuthorDto, CancellationToken cancellationToken)
        {
            await _genericRepository.AddAsync(_mapper.Map<CreateAuthorDto, Author>(createAuthorDto), cancellationToken);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(AuthorDto authorDto, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<AuthorDto, Author>(authorDto);
            _genericRepository.Update(author);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _genericRepository.FindByIdAsync(cancellationToken, id);

            if (author == null) throw new ArgumentException(ExceptionMessages.EntityNotFound);

            author.IsRemoved = true;
            _genericRepository.Update(author);
            await _genericRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
