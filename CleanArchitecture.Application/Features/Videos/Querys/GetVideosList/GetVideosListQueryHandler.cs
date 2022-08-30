using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideosVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IUnitOfWork @object, IMapper mapper)
        {
            Object = @object;
            Mapper = mapper;
        }

        public GetVideosListQueryHandler(IVideoRepository videoRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_videoRepository = videoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork Object { get; }
        public IMapper Mapper { get; }

        public async Task<List<VideosVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            //var videoList = await _videoRepository.GetAllVideosByUserName(request.userName!);
            var videoList = await _unitOfWork.VideoRepository.GetAllVideosByUserName(request.userName);

            return _mapper.Map<List<VideosVm>>(videoList);
        }
    }
}
