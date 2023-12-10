using eTickets.Data.Base;
using eTickets.Data.MovieModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
    {
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
        {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext appDbContext) : base(appDbContext)
            {
            _context = appDbContext;
            }

        public async Task AddNewMovieAsync(NewMovieVM movie)
            {
            //Add movie into database
            var newMovie = new Movie()
                {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                CinemaId = movie.CinemaId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                ProducerId = movie.ProducerId
                };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add movie actors
            foreach(var actorId in movie.ActorIds)
                {
                var newActorMovie = new Actor_Movie()
                    {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                    };
                await _context.Actors_Movies.AddAsync(newActorMovie);
                }
            await _context.SaveChangesAsync();
            }

        public async Task<Movie> GetMovieByIdAsync(int id)
            {
            var movieDetails = await _context.Movies.Include(c => c.Cinema)
                                              .Include(p => p.Producer)
                                              .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                                              .FirstOrDefaultAsync(n => n.Id == id);
            return movieDetails;
                    
            }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownValues()
            {
            //var response = new NewMovieDropdownVM();
            //response.actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync();
            //response.producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync();
            //response.cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();

            var response = new NewMovieDropdownVM()
                {
                actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync(),
                cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync()
                };

            return response;
            }

        public async Task UpdateMovieAsync(NewMovieVM data)
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

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
                {
                var newActorMovie = new Actor_Movie()
                    {
                    MovieId = data.Id,
                    ActorId = actorId
                    };
                await _context.Actors_Movies.AddAsync(newActorMovie);
                }
            await _context.SaveChangesAsync();
            }
        //Get : Movie/Delete/1
        public async Task DeleteMovieAsync(int movieId)
            {
            // Find the movie by Id
            var movieToDelete = await _context.Movies.FindAsync(movieId);

            if (movieToDelete != null)
                {
                // Remove movie actors
                var actorMoviesToDelete = _context.Actors_Movies.Where(am => am.MovieId == movieId);
                _context.Actors_Movies.RemoveRange(actorMoviesToDelete);

                // Remove the movie
                _context.Movies.Remove(movieToDelete);

                // Save changes to the database
                await _context.SaveChangesAsync();
                }
            }

        }
    }
