using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorServices : EntityBaseRepsitory<Actor> ,  IActorServices
    {
        private readonly AppDbContext _context;

        public ActorServices(AppDbContext context) : base(context) { }
         
    }
}
