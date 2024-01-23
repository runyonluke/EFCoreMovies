using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using EFCoreMovies.Utilities;
using EFCoreMovies.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using EFCoreMovies.Entities;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get()
        {
            // use AsNoTracking for read only queries
            return await context.Actors
                .AsNoTracking()
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {
            // use AsNoTracking for read only queries
            return await context.Actors.Select(a => a.Id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreationDTO actorCreationDTO)
        {
            var actor = mapper.Map<Actor>(actorCreationDTO);

            context.Add(actor);
            context.SaveChangesAsync();

            return Ok();
        }

        // connected model
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActorCreationDTO actorCreationDTO, int id)
        {
            var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            var actorDB = mapper.Map(actorCreationDTO, actor);
            await context.SaveChangesAsync();
            
            return Ok();
        }

        // disconnected model
        [HttpPut("disconnected/{id:int}")]
        public async Task<ActionResult> PutDisconnected(ActorCreationDTO actorCreationDTO, int id)
        {
            var existsActor = await context.Actors.AnyAsync(p => p.Id == id);

            if (!existsActor)
            {
                return NotFound();
            }

            var actor = mapper.Map<Actor>(actorCreationDTO);
            actor.Id = id;

            context.Update(actor);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
