using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eTickets.Data.Services
{
    public class MovieService : EntityBaseRepsitory<Movie>, IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(MovieViewModel data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,


            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorsId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };

                await _context.Actor_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();




        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(x=>x.Producer)
                .Include(m=>m.Cinema)
                .Include(am=>am.Actors_Movies).ThenInclude(a=>a.Actor)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return  movieDetails;
        }



        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValue()
        {
            var respone = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(x => x.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(x => x.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(x => x.Name).ToListAsync()
            };
            return respone;
        }

        public async Task UpdateMovieAsync(MovieViewModel data)
        {
         
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
             
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;

                await _context.SaveChangesAsync();
            }

            var existingActorsDb = _context.Actor_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actor_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

          
            foreach (var actorId in data.ActorsId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actor_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
