

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastucture.Repositories
{
    public class VideoRepository : RepositorysBase<Video>, IVideoRepository
    {   
        public VideoRepository(StreamerDbContext _streamerDb) : base(_streamerDb) { }

        public async Task<IEnumerable<Video>> GetAllVideosByUserName(string userName)
        {
            var listOfVideos = await _streamerDb.Video.Where(x => x.CreatedBy == userName).ToListAsync();

            return listOfVideos;
        }

        public async Task<Video> GetVideoByName(string nombreVideo)
        {
             var video = await _streamerDb.Video.Where(x => x.Name == nombreVideo).FirstOrDefaultAsync();

                return video;
        }
    }
}
