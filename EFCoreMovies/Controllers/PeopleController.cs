using AutoMapper;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeopleController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await context.People
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SendMessages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return NotFound();
            }

            return person;
        }
    }
}
