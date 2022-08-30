using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastucture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            //_streamerRepository = streamerRepository;
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);
            //var streamer = await _streamerRepository.AddAsync(streamerEntity);
           var result = await _unitOfWork.Complete();

            if(result <= 0)
            {
                throw new Exception("No se pudo insertar el record en nuestra base");
            }

            _logger.LogInformation($"Streamer {streamerEntity.StreamerId} Fue creado con exito");
            await SendEmail(streamerEntity);

            return streamerEntity.StreamerId;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "yairsienra@gmail.com",
                body = "El streamer se creo correctamente",
                Asunto = "Nuevo streamer"

            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch
            {
                _logger.LogError($"Errores enviando el email");
            }

        }
    }
}
