using eTickets.Data.Base;
using eTickets.Data.MovieModels;
using eTickets.Models;

namespace eTickets.Data.Services
    {
    public interface IMoviesService : IEntityBaseRepository<Movie>
        {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownVM> GetNewMovieDropdownValues();
        Task AddNewMovieAsync(NewMovieVM movie);
        Task UpdateMovieAsync(NewMovieVM movie);
        Task DeleteMovieAsync(int id);
        }
    }
