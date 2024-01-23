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
                .Where(g => !g.IsDeleted)
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

        [HttpPost("add2")]
        public async Task<ActionResult> Add2(int id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Name += " 2";
            await context.SaveChangesAsync();
 
            return Ok();
        }


        [HttpPost("several")]
        public async Task<ActionResult> Post(GenreCreationDTO[] genresDTO)
        {
            var genres = mapper.Map<Genre[]>(genresDTO);

            // for adding multiple, this is an option, but it is not good
            //foreach (var genre in genres)
            //{
            //    context.Add(genre);
            //}

            // a better option than above
            context.AddRange(genres);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(c => c.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            context.Remove(genre);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("softdelete/{id:int}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(c => c.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.IsDeleted = true;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
