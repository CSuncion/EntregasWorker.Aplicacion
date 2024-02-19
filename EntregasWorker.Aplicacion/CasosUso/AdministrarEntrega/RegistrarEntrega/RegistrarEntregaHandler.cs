using AutoMapper;
using EntregasWorker.Aplicacion.Common;
using EntregasWorker.Dominio.Repositorios;
using MediatR;


namespace EntregasWorker.Aplicacion.CasosUso.AdministrarEntrega.RegistrarEntrega
{
    public  class RegistrarEntregaHandler : IRequestHandler<RegistrarEntregaRequest, IResult>
    {
        private readonly IEntregaRepository _entregaRepository;
        private readonly IMapper _mapper;
        public RegistrarEntregaHandler(IEntregaRepository entregaRepository, IMapper mapper)
        {
            _entregaRepository = entregaRepository;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(RegistrarEntregaRequest request, CancellationToken cancellationToken)
        {
            IResult result = null;

            try
            {
                var entrega = _mapper.Map<Dominio.Models.Entrega>(request);


                var adicionar = await _entregaRepository.Adicionar(entrega);

                if (adicionar)
                    result = new SuccessResult();
                else
                    result = new FailureResult();

                return result;
            }
            catch (Exception ex)
            {
                return new FailureResult();
            }

        }
    }
}
