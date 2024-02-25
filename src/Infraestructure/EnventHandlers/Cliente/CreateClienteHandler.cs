using ApplicationCore.Commands.Cliente;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EnventHandlers.Cliente
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateClienteHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var u = new CreateClienteCommand();
            u.nombre = request.nombre;
            u.apellido = request.apellido;

            var us = _mapper.Map<Domain.Entities.cliente>(u);
            await _context.clientes.AddAsync(us);
            await _context.SaveChangesAsync();
            return new Response<int>(us.id, "Registro Creado");

        }
    }
}
