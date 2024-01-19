using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using EFCoreMovies.Utilities;
using EFCoreMovies.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page = 1, int recordsToTake = 2)
        {
            // use AsNoTracking for read only queries
            return await context.Actors
                .AsNoTracking()
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
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
