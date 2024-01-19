using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using EFCoreMovies.Utilities;
using EFCoreMovies.DTOs;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController
    {
        private readonly ApplicationDbContext context;

        public ActorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page = 1, int recordsToTake = 2)
        {
            // use AsNoTracking for read only queries
            return await context.Actors
                .AsNoTracking()
                .Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth})
                .OrderBy(a => a.Name)
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {
            // use AsNoTracking for read only queries
            return await context.Actors.Select(a => a.Id).ToListAsync();
        }
    }
}
