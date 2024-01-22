using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreMovies.Utilities;
using EFCoreMovies.DTOs;
using AutoMapper;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            // use AsNoTracking for read only queries
            return await context.Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .ToListAsync();
        }


        [HttpGet("first")]
        public async Task<ActionResult<Genre>> GetFirst()
        {
            var genre = await context.Genres.FirstOrDefaultAsync(g => g.Name.Contains("x"));

            if (genre is null)
            {
                return NotFound();
            }
            return genre;
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<Genre>> Filter(string name)
        {
            return await context.Genres.Where(g => g.Name.Contains(name)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            // remember that this isn't actually inserting the record in the database
            // EF tracks entities with statuses
            // EF is smart, you don't need to specify "context.Genres.Add()"
            context.Add(genre); //marking genre as added.

            // we are telling EFCore to change all entities it is tracking
            // actually inserts here
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
