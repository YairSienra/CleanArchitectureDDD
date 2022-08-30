using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastucture.Persistence;
using MediatR;

namespace CleanArchitecture.Infrastucture.Repositories
{
    public class StreamerRepository : RepositorysBase<Streamer>, IStreamerRepository
    {   
    

        public StreamerRepository(StreamerDbContext StreamerDb) : base(StreamerDb)
        {

        }

        public async Task<Streamer> GetAsyncById(int id)
        {
            var idStreamer = _streamerDb.Streamer.Where(x => x.Id == id).FirstOrDefault();
            

            if (idStreamer != null)
            {
                return idStreamer;
            } else
            {
                throw new Exception("No se encontro el sigueinte streamer");
                return null;
            }

            throw new Exception("Exito al borrar");
        }
    }
}
