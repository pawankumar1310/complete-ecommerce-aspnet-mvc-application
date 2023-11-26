using eTickets.Models;

namespace eTickets.Data.IServices
    {
    public interface IActorService
        {
        Task<IEnumerable<Actor>> GetallAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task<Actor> UpdateAsync(int id, Actor newActor);
        Task DeleteAsync(int id);
        }
    }
