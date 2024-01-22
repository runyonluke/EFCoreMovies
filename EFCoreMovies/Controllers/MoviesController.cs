using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id){
            var movie = await context.Movies
                .Include(m => m.Genres.OrderByDescending(g => g.Name).Where(g => !g.Name.Contains("m")))
                .Include(m => m.CinemasHalls.OrderByDescending(ch => ch.Cinema.Name))
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MoviesActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(m => m.Id).ToList();

            return movieDTO;
        }

        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id)
        {
            var movieDTO = await context.Movies
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(m => m.Id).ToList();

            return movieDTO;
        }

        [HttpGet("selectloading/{id:int}")]
        public async Task<ActionResult> GetSelectLoading(int id)
        {
            var movieDTO = await context.Movies.Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                Genres = m.Genres.Select(g => g.Name).OrderByDescending(n => n).ToList()
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }

            return Ok(movieDTO);
        }

        // not usually used because of performance
        [HttpGet("explicitLoading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetExplicit(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var genresCount = await context.Entry(movie).Collection(p => p.Genres).Query().CountAsync();
            var movieDTO = mapper.Map<MovieDTO>(movie);

            return Ok(new
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                GenresCount = genresCount
            });
        }

        // not usually used because of performance
        // queries database too many times, n + 1 problem
        [HttpGet("lazyloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetLazyLoading(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("groupedByCinema")]
        public async Task<ActionResult> GetGroupedByCinema()
        {
            var groupedMovies = await context.Movies.GroupBy(m => m.InCinemas).Select(g => new
            {
                InCinemas = g.Key,
                Count = g.Count(),
                Movies = g.ToList()
            }).ToListAsync();

        return Ok(groupedMovies);
        }

        [HttpGet("groupByGenresCount")]
        public async Task<ActionResult> GetGroupedGenresCount()
        {
            var genresCount = await context.Movies.GroupBy(m => m.Genres.Count()).Select(g => new
            {
                Count = g.Key,
                Titles = g.Select(m => m.Title),
                Genres = g.Select(m => m.Genres).SelectMany(a => a).Select(ge => ge.Name).Distinct()
            }).ToListAsync();

            return Ok(genresCount);
        }

        // FromQuery means that the values from this class will come from Query strings
        // needed because MovieFilterDTO is a complex type
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Filter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();

            //only run if there is a title
            if (!string.IsNullOrEmpty(movieFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDTO.Title));
            }

            // Make sure InCinemas exists in both movieFilterDTO and movieQueryable
            if (movieFilterDTO.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InCinemas);
            }

            // We know they are upcoming if they occur after today
            if (movieFilterDTO.UpcomingReleases)
            {
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > DateTime.Today);
            }

            // Find only genre's with a specific Id
            if (movieFilterDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDTO.GenreId));
            }

            var movies = await moviesQueryable.Include(m => m.Genres).ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieCreationDTO movieCreationDTO)
        {
            var movie = mapper.Map<Movie>(movieCreationDTO);

            movie.Genres.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            movie.CinemasHalls.ForEach(ch => context.Entry(ch).State = EntityState.Unchanged);

            if (movie.MoviesActors is not null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }

            context.Add(movie);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
