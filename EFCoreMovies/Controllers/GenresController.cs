using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public GenresController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            // use AsNoTracking for read only queries
            return await context.Genres.AsNoTracking().ToListAsync();
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
    }
}
