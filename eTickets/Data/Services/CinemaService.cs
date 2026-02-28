using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class CinemaService : EntityBaseRepsitory<Cinema>, ICinemaService
    {
        public CinemaService(AppDbContext context) : base(context)
        {
        }
    }
}
