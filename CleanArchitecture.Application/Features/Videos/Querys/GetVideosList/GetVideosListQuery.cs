using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<VideosVm>>
    {
        public string userName { get; set; } = string.Empty;

        public GetVideosListQuery(string userName)
        {
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
