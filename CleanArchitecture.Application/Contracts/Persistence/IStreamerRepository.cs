using CleanArchitecture.Domain;


namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IStreamerRepository : IAsyncRepository<Streamer>
    {
        Task<Streamer> GetAsyncById(int id);
    }
}
