using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exeptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger, IUnitOfWork unitOfWork)
        {
           // _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;   
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamer = await _unitOfWork.StreamerRepository.GetByIdAsync(request.StreamerId);

            if(streamer == null)
            {
                _logger.LogError($"{request.StreamerId} no existe en nuestro sistema");

                throw new NotFoundExeptions(nameof(Streamer), request.StreamerId);
            }

            //  await _streamerRepository.DeleteAsync(streamer);
            _unitOfWork.StreamerRepository.DeleteEntity(streamer);
           await  _unitOfWork.Complete();

            _logger.LogInformation($"El {streamer} se borro correctamente");

            return Unit.Value;
        }
    }
}
