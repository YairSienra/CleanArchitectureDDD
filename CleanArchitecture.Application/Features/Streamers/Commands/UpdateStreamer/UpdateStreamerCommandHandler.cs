using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exeptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {   
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IStreamerRepository _stremaerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository stremaerRepository, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            //_stremaerRepository = stremaerRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            // var streamer = await _stremaerRepository.GetByIdAsync(request.StreamerId);
            var streamer = await _unitOfWork.StreamerRepository.GetByIdAsync(request.StreamerId);
            if(streamer == null)
            {
                _logger.LogError($"No se encontro el streamer  {request.StreamerId}");
                throw new NotFoundExeptions(nameof(streamer), request.StreamerId);
            }

            //_mapper.Map(request, streamer, typeof(UpdateStreamerCommand), typeof(Streamer));

            streamer.Name = request.Name ?? streamer.Name;
            streamer.Url = request.Url ?? streamer.Url; 

            // await _stremaerRepository.UpdateAsync(streamer);
            _unitOfWork.StreamerRepository.UpdateEntity(streamer);
            await _unitOfWork.Complete();



            _logger.LogInformation($"La operacion fue exitosa actualizsando el streamer {streamer.StreamerId}");

            return Unit.Value;
        }


    }
}
