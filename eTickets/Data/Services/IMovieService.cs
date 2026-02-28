using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMovieService : IEntityBaseRepsitory<Movie>
    {
        Task<Movie>GetMovieByIdAsync(int id);    
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValue();

        Task AddNewMovieAsync(MovieViewModel data);
        Task UpdateMovieAsync(MovieViewModel data);
    }
}
